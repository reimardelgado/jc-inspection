using Backend.Application.Commands.InspectionCommands;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Backend.API.DTOs.Requests.InspectionResultRequests;

public class UpdateInspectionResultRequest
{
    public string InspectionId { get; set; }
    public string FormTemplateId { get; set; }
    public string? IdZoho { get; set; }
    public string? SectionId { get; set; }
    public string? SectionName { get; set; }
    public string? ItemId { get; set; }
    public string? ItemName { get; set; }
    public string? ItemDatatype { get; set; }
    public string? ItemValue { get; set; }
    public string? CatalogId { get; set; }
    public string? ItemComment { get; set; }

    public UpdateInspectionResultRequest(string inspectionId, string formTemplateId, string? idZoho,
        string? sectionId, string? sectionName, string? itemId, string? itemName,
        string? itemDatatype, string? itemValue, string? catalogId, string? itemComment)
    {
        InspectionId = inspectionId;
        FormTemplateId = formTemplateId;
        IdZoho = idZoho;
        SectionId = sectionId;
        SectionName = sectionName;
        ItemId = itemId;
        ItemName = itemName;
        ItemDatatype = itemDatatype;
        ItemValue = itemValue;
        CatalogId = catalogId;
        ItemComment = itemComment;
    }

    public UpdateInspectionResultCommand ToApplicationRequest(string id)
    {
        var catalogId = !string.IsNullOrEmpty(CatalogId) ? Guid.Parse(CatalogId) : default(Guid?);
        return new UpdateInspectionResultCommand(Guid.Parse(id), Guid.Parse(InspectionId), Guid.Parse(FormTemplateId),
            IdZoho, Guid.Parse(SectionId), SectionName, Guid.Parse(ItemId), ItemName, ItemDatatype, ItemValue, catalogId,
            ItemComment);
    }
}