namespace Backend.Application.DTOs.Responses.ItemResponses;

public class ItemModelResponse
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? DataType { get; set; }
    public string? Value { get; set; }
    public string? Comment { get; set; }
    public Guid? CatalogId { get; set; }
    public string? CatalogName { get; set; }
    public string Status { get; set; }

    public ItemModelResponse(Guid id, string? name, string? dataType, string? value, 
        string? comment, string status)
    {
        Id = id;
        Name = name;
        DataType = dataType;
        Value = value;
        Comment = comment;
        Status = status;
    }
}