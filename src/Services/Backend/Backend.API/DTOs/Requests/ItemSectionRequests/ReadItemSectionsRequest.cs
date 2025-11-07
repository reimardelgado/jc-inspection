using Backend.Application.Queries.ItemSectionQueries;
using Shared.API.DTOs;

namespace Backend.API.DTOs.Requests.ItemSectionRequests;

public class ReadItemSectionsRequest : BaseFilterDto
{
    public Guid? FormTemplateId { get; set; }
    public string? QueryParam { get; set; }

    public ReadItemSectionsQuery ToApplicationRequest()
    {
        return new ReadItemSectionsQuery(FormTemplateId, QueryParam, LoadChildren, IsPagingEnabled, Page, PageSize);
    }

}