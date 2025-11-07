using System.Net.Http.Headers;
using System.Text;
using Backend.Application.Configurations;
using Backend.Application.Queries.ZohoQueries;
using Backend.Domain.DTOs.Requests.Zoho;
using Backend.Domain.DTOs.Responses.Zoho;
using Backend.Domain.Interfaces.Services;
using Backend.Domain.MessageHandlers;
using Backend.Domain.SeedWork;
using MediatR;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Shared.Domain.Models;

namespace Backend.Infrastructure.Services;

public class ZohoInspectionService : IZohoInspectionService
{
    private readonly ZohoCrmSettings _zohoCrmSettings;
    private readonly HttpClient _httpClient;
    private readonly IMediator _mediator;

    public ZohoInspectionService(IOptions<ZohoCrmSettings> zohoCrmSettings, HttpClient httpClient, IMediator mediator)
    {
        _zohoCrmSettings = zohoCrmSettings.Value;
        _httpClient = httpClient;
        _mediator = mediator;
    }

    public async Task<string> CreateInZoho(ZohoInspection entity, CancellationToken cancellationToken)
    {
        var token = await _mediator.Send(new ReadZohoTokenQuery());
        var urlBase = $"{_zohoCrmSettings.ZohoUrlApi}/{_zohoCrmSettings.ModuleSettings.InspectionsTc}" ;
        var url = urlBase;
        
        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Zoho-oauthtoken", token.Value!.AccessToken);
        var jsonBody = JsonConvert.SerializeObject(entity);
        var request = new StringContent(jsonBody, Encoding.UTF8, "application/json");
        
        using var httpResponse = await _httpClient.PostAsync(url, null, cancellationToken).ConfigureAwait(false);

        if (!httpResponse.IsSuccessStatusCode)
            return EntityResponse<string>.Error(MessageHandler.ZohoInspectionErrorCreating)!;

        var rawContent = await httpResponse.Content.ReadAsStringAsync(cancellationToken);
        var response = JsonConvert.DeserializeObject<ZohoCreateRecordResponse>(rawContent);

        if (response!.data.ElementAt(0).code == "SUCCESS")
        {
            return response.data.ElementAt(0).details.id;
        }
        return string.Empty;
    }

    public async Task<ZohoInspection> ReadInspectionZoho(string idZoho, string inspectionId, CancellationToken cancellationToken)
    {
        try
        {
            var token = await _mediator.Send(new ReadZohoTokenQuery());
            var urlBase = $"{_zohoCrmSettings.ZohoUrlApi}/{_zohoCrmSettings.ModuleSettings.InspectionsTc}" ;
            var url = $"{urlBase}/{idZoho}";
        
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Zoho-oauthtoken", token.Value!.AccessToken);
            
            using var httpResponse = await _httpClient.GetAsync(url, cancellationToken).ConfigureAwait(false);

            if (!httpResponse.IsSuccessStatusCode)
                return EntityResponse<ZohoInspection>.Error(MessageHandler.ZohoInspectionErrorCreating)!;

            var rawContent = await httpResponse.Content.ReadAsStringAsync(cancellationToken);
            var response = JsonConvert.DeserializeObject<ZohoReadInspectionResponse>(rawContent);

            if (response!.data.Any() && response!.data.ElementAt(0) != null)
            {
                var elem = response!.data.ElementAt(0);
                var zohoInspection = new ZohoInspection();
                zohoInspection.Id = elem.id;
                zohoInspection.IdInspection = inspectionId.ToString();
                zohoInspection.Name = elem.Name;
                zohoInspection.DealNumber = elem.Deal_Number;
                zohoInspection.Inspector = elem.Inspector;
                zohoInspection.Template = elem.Template;
                zohoInspection.InspectionDate = elem.Inspection_Date;
                zohoInspection.Status = elem.Status;
                zohoInspection.UrlInspectionReport = elem.Url_Inspection_Report;
                
                return zohoInspection;
            }
            return null;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }

    public async Task<string> SendInspectionDataToZoho(string inspectionIdZoho, string inspectionId,  string urlReport, CancellationToken cancellationToken)
    {
        try
        {
            var zohoInspection = await ReadInspectionZoho(inspectionIdZoho, inspectionId, cancellationToken);
            if (zohoInspection is null)
            {
                return EntityResponse<string>.Error(MessageHandler.ZohoInspectionErrorUpdate)!;
            }
            var token = await _mediator.Send(new ReadZohoTokenQuery());
            var urlBase = $"{_zohoCrmSettings.ZohoUrlApi}/{_zohoCrmSettings.ModuleSettings.InspectionsTc}" ;
            var url = $"{urlBase}/{inspectionIdZoho}";
        
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Zoho-oauthtoken", token.Value!.AccessToken);
            var data = new ZohoInspectionData();
            zohoInspection.Status = InspectionStatusEnum.Completed;
            zohoInspection.UrlInspectionReport = urlReport;
            data.Data.Add(zohoInspection);
            var jsonBody = JsonConvert.SerializeObject(data);
            var request = new StringContent(jsonBody, Encoding.UTF8, "application/json");
        
            using var httpResponse = await _httpClient.PutAsync(url, request, cancellationToken).ConfigureAwait(false);

            if (!httpResponse.IsSuccessStatusCode)
                return EntityResponse<string>.Error(MessageHandler.ZohoInspectionErrorUpdate)!;

            var rawContent = await httpResponse.Content.ReadAsStringAsync(cancellationToken);
            var response = JsonConvert.DeserializeObject<ZohoCreateRecordResponse>(rawContent);

            if (response!.data.ElementAt(0).code == "SUCCESS")
            {
                return response.data.ElementAt(0).details.id;
            }
            return string.Empty;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return e.Message;
        }
    }
}