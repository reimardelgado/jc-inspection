using Backend.Application.Queries.CatalogQueries;
using Shared.API.DTOs;

namespace Backend.API.DTOs.Requests.CatalogRequests;

public class ReadCatalogsRequest : BaseFilterDto
{
    public string? QueryParam { get; set; }

    public ReadCatalogsQuery ToApplicationRequest()
    {
        return new ReadCatalogsQuery(QueryParam, LoadChildren, IsPagingEnabled, Page, PageSize);
    }

}