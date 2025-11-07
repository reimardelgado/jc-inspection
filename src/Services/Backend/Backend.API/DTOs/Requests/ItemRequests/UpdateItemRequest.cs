using Backend.Application.Commands.ItemCommands;
using Backend.Application.Commands.ItemSectionCommands;
using Backend.Domain.SeedWork;

namespace Backend.API.DTOs.Requests.ItemRequests;

public class UpdateItemRequest
{
    public Guid ItemSectionId { get; set; }
    public Guid FormTemplateId { get; set; }
    public string? Name { get; set; }
    public string? DataType { get; set; }
    public string? Value { get; set; }
    public string? Comment { get; set; }
    public Guid? CatalogId { get; set; }

    public UpdateItemRequest(Guid itemSectionId, Guid formTemplateId, string? name, string? dataType, string? value, string? comment, Guid? catalogId)
    {
        ItemSectionId = itemSectionId;
        FormTemplateId = formTemplateId;
        Name = name;
        DataType = dataType;
        Value = value;
        Comment = comment;
        CatalogId = catalogId;
    }

    public UpdateItemCommand ToApplicationRequest(Guid id)
    {
        return new UpdateItemCommand(id, ItemSectionId, FormTemplateId, Name, DataType, Value, Comment, CatalogId);
    }
}