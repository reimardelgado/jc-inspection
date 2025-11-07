namespace Backend.Application.DTOs;

public class ItemDto
{
    public string ItemName { get; set; }
    public string ItemDatatype { get; set; }
    public string? ItemCatalogId { get; set; }
    public string? ItemCatalogName { get; set; }

    public ItemDto(string itemName, string itemDatatype, string? itemCatalogId, string? itemCatalogName)
    {
        ItemName = itemName;
        ItemDatatype = itemDatatype;
        ItemCatalogId = itemCatalogId;
        ItemCatalogName = itemCatalogName;
    }
}