
using Backend.Application.DTOs.Responses.CatalogResponses;

namespace Backend.Application.Queries.CatalogQueries
{
    public class ReadCatalogQuery : IRequest<EntityResponse<CatalogResponse>>
    {
        public Guid CatalogId { get; }

        public ReadCatalogQuery(Guid id)
        {
            CatalogId = id;
        }
    }
}