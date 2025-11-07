using Backend.Application.Queries.CatalogValueQueries;
using Shared.API.DTOs;

namespace Backend.API.DTOs.Requests.CatalogValueRequests;

public class ReadCatalogValueRequest : BaseFilterDto
{
    private Guid CatalogValueId { get; }

    public ReadCatalogValueRequest(Guid optionValueId)
    {
        CatalogValueId = optionValueId;
    }

    public ReadCatalogValueQuery ToApplicationRequest()
    {
        return new ReadCatalogValueQuery(CatalogValueId);
    }
}