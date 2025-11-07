using Backend.Application.Queries.ItemQueries;
using Shared.API.DTOs;

namespace Backend.API.DTOs.Requests.ItemRequests;

public class ReadItemRequest : BaseFilterDto
{
    private Guid ItemId { get; }

    public ReadItemRequest(Guid itemId)
    {
        ItemId = itemId;
    }

    public ReadItemQuery ToApplicationRequest()
    {
        return new ReadItemQuery(ItemId);
    }
}