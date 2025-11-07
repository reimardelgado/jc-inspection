
using Backend.Application.DTOs.Responses.CatalogValueResponses;

namespace Backend.Application.Queries.CatalogValueQueries
{
    public class ReadAllCatalogValuesQuery : IRequest<EntityResponse<List<CatalogValueResponse>>>
    {
        public ReadAllCatalogValuesQuery()
        {
        }
    }
}