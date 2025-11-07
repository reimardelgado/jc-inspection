using Backend.Application.Queries.CatalogValueQueries;
using Shared.API.DTOs;

namespace Backend.API.DTOs.Requests.CatalogValueRequests;

public class ReadCatalogValuesRequest : BaseFilterDto
{
    public Guid? CatalogId { get; set; }
    public string? QueryParam { get; set; }

    public ReadCatalogValuesQuery ToApplicationRequest()
    {
        return new ReadCatalogValuesQuery(CatalogId, QueryParam, LoadChildren, IsPagingEnabled, Page, PageSize);
    }

}