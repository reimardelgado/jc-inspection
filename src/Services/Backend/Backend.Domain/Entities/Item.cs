using Backend.Domain.SeedWork;

namespace Backend.Domain.Entities;

public class Item : BaseEntity
{
    public string? NameVariable { get; set; }
    public Guid ItemSectionId { get; set; }
    public ItemSection? ItemSection { get; set; }

    public Guid FormTemplateId { get; set; }
    public FormTemplate? FormTemplate { get; set; }
    public string? Name { get; set; }
    public string? DataType { get; set; }
    public string? Value { get; set; }
    public string? Comment { get; set; }
    public Guid? CatalogId { get; set; }
    public Catalog? Catalog { get; set; }
    public string Status { get; set; } = StatusEnum.Active;
    
    public Item(Guid itemSectionId, Guid formTemplateId, string? name, string? dataType, string? value, string? comment, Guid? catalogId)
    {
        ItemSectionId = itemSectionId;
        FormTemplateId = formTemplateId;
        Name = name;
        DataType = dataType;
        Value = value;
        Comment = comment;
        CatalogId = catalogId;
        NameVariable = $"$item_{Id}";
    }
}