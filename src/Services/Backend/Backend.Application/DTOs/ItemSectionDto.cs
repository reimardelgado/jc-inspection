namespace Backend.Application.DTOs;

public class ItemSectionDto
{
    public string Name { get; set; }
    public List<ItemDto> Items { get; set; }

    public ItemSectionDto(string name, List<ItemDto> items)
    {
        Name = name;
        Items = items;
    }
}