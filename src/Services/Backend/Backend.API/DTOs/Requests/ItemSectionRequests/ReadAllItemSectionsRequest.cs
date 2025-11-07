using Backend.Application.Queries.ItemSectionQueries;
using Shared.API.DTOs;

namespace Backend.API.DTOs.Requests.ItemSectionRequests;

public class ReadAllItemSectionsRequest
{
    public Guid FormTemplateId { get; set; }

    public ReadAllItemSectionsQuery ToApplicationRequest()
    {
        return new ReadAllItemSectionsQuery(FormTemplateId);
    }

}