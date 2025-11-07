using Backend.Application.Queries.ItemQueries;
using Shared.API.DTOs;

namespace Backend.API.DTOs.Requests.ItemRequests;

public class ReadItemsRequest : BaseFilterDto
{
    public Guid? FormTemplateId { get; set; }
    public Guid? ItemSectionId { get; set; }
    public string? QueryParam { get; set; }

    public ReadItemsQuery ToApplicationRequest()
    {
        return new ReadItemsQuery(FormTemplateId, ItemSectionId, QueryParam, LoadChildren, IsPagingEnabled, Page, PageSize);
    }

}