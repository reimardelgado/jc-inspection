using Backend.Application.Queries.ItemQueries;
using Shared.API.DTOs;

namespace Backend.API.DTOs.Requests.ItemRequests;

public class ReadAllItemsRequest
{
    public Guid FormTemplateId { get; set; }
    public Guid ItemSectionId { get; set; }

    public ReadAllItemsQuery ToApplicationRequest()
    {
        return new ReadAllItemsQuery(FormTemplateId, ItemSectionId);
    }

}