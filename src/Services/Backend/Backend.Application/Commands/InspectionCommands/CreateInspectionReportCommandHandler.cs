using Backend.Application.Configurations;
using Backend.Application.Queries.PhotoQueries;
using Backend.Application.Specifications.InspectionResultSpecs;
using Backend.Application.Specifications.InspectionSpecs;
using Backend.Application.Specifications.ItemSectionSpecs;
using Backend.Application.Specifications.PhotoSpecs;
using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.Extensions.Options;

namespace Backend.Application.Commands.InspectionCommands;

public class
    CreateInspectionReportCommandHandler : IRequestHandler<CreateInspectionReportCommand, EntityResponse<byte[]>>
{
    #region Constructor & Properties

    private readonly IRepository<Inspection> _inspectionRepository;
    private readonly IRepository<InspectionResult> _inspectionResultRepository;
    private readonly IRepository<Photo> _photoRepository;
    private readonly IRepository<ItemSection> _itemSectionRepository;
    private readonly IMediator _mediator;
    private readonly IConverter _converter;
    private Inspection _inspection;
    private List<InspectionResult>? _inspectionResults;
    private readonly ComunSettings _comunSettings;

    public CreateInspectionReportCommandHandler(IRepository<Inspection> inspectionRepository,
        IRepository<InspectionResult> inspectionResultRepository, IRepository<Photo> photoRepository,
        IMediator mediator, IConverter converter, IRepository<ItemSection> itemSectionRepository, 
        IOptions<ComunSettings> comunSettings)
    {
        _inspectionRepository = inspectionRepository;
        _inspectionResultRepository = inspectionResultRepository;
        _photoRepository = photoRepository;
        _mediator = mediator;
        _converter = converter;
        _itemSectionRepository = itemSectionRepository;
        _comunSettings = comunSettings.Value;
    }

    #endregion


    public async Task<EntityResponse<byte[]>> Handle(CreateInspectionReportCommand command,
        CancellationToken cancellationToken)
    {
        // Repository
        var inspectionSpec = new InspectionSpec(command.InspectionId);
        _inspection = await _inspectionRepository.GetBySpecAsync(inspectionSpec, cancellationToken);
        if (_inspection is null)
        {
            return EntityResponse<byte[]>.Error(MessageHandler.ErrorGeneratingReport);
        }

        var spec = new InspectionResultByResultSpec(_inspection.Id, _inspection.FormTemplateId);
        _inspectionResults = await _inspectionResultRepository.ListAsync(spec, cancellationToken);
        if (!_inspectionResults.Any())
        {
            return EntityResponse<byte[]>.Error(MessageHandler.ErrorGeneratingReport);
        }

        var html = "";
        html += GenerateHeaderHtml();

        var sectionSpec = new ItemSectionSpec(_inspection.FormTemplateId);
        var sections = await _itemSectionRepository.ListAsync(sectionSpec, cancellationToken);
        foreach (var sec in sections)
        {
            html += await GenerateSectionHtml(sec, command.ContentRootPath, cancellationToken);
        }


        GlobalSettings globalSettings = new GlobalSettings();
        globalSettings.ColorMode = ColorMode.Color;
        globalSettings.Orientation = Orientation.Portrait;
        globalSettings.PaperSize = PaperKind.A4;
        globalSettings.Margins = new MarginSettings { Top = 20, Bottom = 20 };
        ObjectSettings objectSettings = new ObjectSettings();
        //objectSettings.PagesCount = true;
        objectSettings.HtmlContent = html;
        WebSettings webSettings = new WebSettings();
        webSettings.DefaultEncoding = "utf-8";
        HeaderSettings headerSettings = new HeaderSettings();
        headerSettings.FontSize = 12;
        headerSettings.FontName = "Ariel";
        headerSettings.Line = false;
        FooterSettings footerSettings = new FooterSettings();
        footerSettings.FontSize = 12;
        footerSettings.FontName = "Ariel";
        footerSettings.Center = "Report.";
        footerSettings.Line = true;
        objectSettings.HeaderSettings = headerSettings;
        objectSettings.FooterSettings = footerSettings;
        objectSettings.WebSettings = webSettings;
        HtmlToPdfDocument htmlToPdfDocument = new HtmlToPdfDocument()
        {
            GlobalSettings = globalSettings,
            Objects = { objectSettings },
        };
        var result = _converter.Convert(htmlToPdfDocument);

        return EntityResponse.Success(result);
    }

    //-webkit-transform: rotate(90deg); rotar imagen

    #region Private Methods

    private string GenerateHeaderHtml()
    {
        var html = $@"<!DOCTYPE html>
                    <html lang='en'>
                    <p>&nbsp;</p>
                    <div style='text-align: right'>
                        <img style='text-align: center; width: 25%;' src='https://ciemtelcom-surveys.com/surveys.png'>
                    </div>
                    <h3 style='text-align: center; color: #086480;'><span style='border-bottom: 4px solid #576905;'>TECHNICAL REPORT</h3>
                    <table  style='height: 54px; border: 1px solid; width: 100%'>
                    <thead>
                    <tr style='border: 0px solid; background: #576905; color: #fff'>
	                    <td colspan='2'><strong>INFORMATION</strong></td>  
                    </tr>
                    </thead>
                    <tbody>
                        <tr style='border: 0px solid'>
                            <td style='width: 180px; text-align: left;'><strong>Inspection Name:</strong> </td>
                            <td>{_inspection.Name}</td>
                        </tr>
                        <tr style='border: 0px solid'>
                            <td style='width: 180px; text-align: left;'><strong>Deal Number:</strong> </td>
                            <td>{_inspection.DealNumber}</td>
                        </tr>
                        <tr>
                            <td style='width: 180px; text-align: left;'><strong>Inspector:</strong> </td>
                            <td>{_inspection.Inspector.FullName}</td>
                        </tr>
                        <tr>
                            <td style='width: 180px; text-align: left;'><strong>Execution date:</strong> </td>
                            <td>{_inspection.InspectionDate.ToShortDateString()}</td>
                        </tr>
                    </tbody>
                    </table>
                    <br>                    
                <h3>TECHNICAL REVIEW RESULTS</h3>
                <hr>";
        return html;
    }

    private async Task<string> GenerateSectionHtml(ItemSection itemSection, string contentRootPath,
        CancellationToken cancellationToken)
    {
        // var html = $@"<h4>- {itemSection.Name}</h4>
        //         <br>";
        //

        var html = $@"<table style='height: 45px; width: 100%; border: 0px solid'>
                <thead>
                <tr style='border: 1px solid'>
	                <td colspan='3' style='border-bottom: 0px solid; background: #576905; color: #fff; text-align:center'><strong>{itemSection.Name}</strong></td>  
                </tr>
                    <tr style='border: 1px solid'>
                        <td style='width: 45%; text-align: left'><strong>Item</strong></td>
                        <td style='width: 25%; text-align: left'><strong>Value</strong></td>
                        <td style='width: 30%; text-align: left'><strong>Comment</strong></td>
                    </tr>
                </thead>";
        var items = _inspectionResults.Where(x => x.SectionId == itemSection.Id);
        html += $@"<tbody>";
        if (!items.Any())
        {
            html += $@" <tr style='border: 0px solid'><td></td><td></td><td></td></tr>";
        }
        else
        {
            for (int i = 0; i < items.Count(); i++)
            {
                html += $@"<tr style='border: 0px solid'>
  	                    <td>{items.ElementAt(i).ItemName}</td>
                        <td>{items.ElementAt(i).ItemValue}</td>
                        <td>{items.ElementAt(i).ItemComment}</td>
                        </tr>";
            }
        }

        html += $@"</tbody></table><br>";

        var photosRepo =
            await _photoRepository.ListAsync(new PhotoSpec(_inspection.Id, itemSection.Id), cancellationToken);
        if (photosRepo.Any())
        {
            var photos = _mediator.Send(new ReadAllPhotosQuery(_inspection.Id, itemSection.Id, contentRootPath)).Result
                .Value;
            html += $@"<div class='col-md-12'>";
            if (photos != null)
            {
                for (int i = 0; i < photos.Name.Count(); i++)
                {
                    html +=
                        $@"<img style='height: 220px; width: 25%;' src='{_comunSettings.UrlDomain}/inspection/image/{_inspection.DealNumber}/{photos.Name.ElementAt(i)}'>";
                }
            }

            html += $@"</div><br>";
        }

        return html;
    }

    #endregion
}