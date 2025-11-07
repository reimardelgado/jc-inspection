
using Backend.Application.DTOs.Responses.CatalogResponses;

namespace Backend.Application.Queries.CatalogQueries
{
    public class ReadAllCatalogsQuery : IRequest<EntityResponse<List<CatalogResponse>>>
    {
        public ReadAllCatalogsQuery()
        {
        }
    }
}