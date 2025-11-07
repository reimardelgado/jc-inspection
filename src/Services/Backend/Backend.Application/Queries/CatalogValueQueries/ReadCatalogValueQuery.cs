
using Backend.Application.DTOs.Responses.CatalogValueResponses;

namespace Backend.Application.Queries.CatalogValueQueries
{
    public class ReadCatalogValueQuery : IRequest<EntityResponse<CatalogValueResponse>>
    {
        public Guid CatalogValueId { get; }

        public ReadCatalogValueQuery(Guid id)
        {
            CatalogValueId = id;
        }
    }
}