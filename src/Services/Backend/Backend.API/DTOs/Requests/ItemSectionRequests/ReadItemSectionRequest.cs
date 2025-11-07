using Backend.Application.Queries.ItemSectionQueries;
using Shared.API.DTOs;

namespace Backend.API.DTOs.Requests.ItemSectionRequests;

public class ReadItemSectionRequest : BaseFilterDto
{
    private Guid ItemSectionId { get; }

    public ReadItemSectionRequest(Guid itemSectionId)
    {
        ItemSectionId = itemSectionId;
    }

    public ReadItemSectionQuery ToApplicationRequest()
    {
        return new ReadItemSectionQuery(ItemSectionId);
    }
}