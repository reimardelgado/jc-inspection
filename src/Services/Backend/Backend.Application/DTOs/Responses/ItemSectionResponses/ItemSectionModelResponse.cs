using Backend.Application.DTOs.Responses.ItemResponses;

namespace Backend.Application.DTOs.Responses.ItemSectionResponses;

public class ItemSectionModelResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Status { get; set; }
    public List<ItemModelResponse> Items { get; set; }

    public ItemSectionModelResponse()
    {
        Items = new List<ItemModelResponse>();
    }
}