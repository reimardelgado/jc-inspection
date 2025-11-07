using Backend.Application.Queries.CatalogQueries;
using Shared.API.DTOs;

namespace Backend.API.DTOs.Requests.CatalogRequests;

public class ReadCatalogRequest : BaseFilterDto
{
    private Guid CatalogId { get; }

    public ReadCatalogRequest(Guid catalogId)
    {
        CatalogId = catalogId;
    }

    public ReadCatalogQuery ToApplicationRequest()
    {
        return new ReadCatalogQuery(CatalogId);
    }
}