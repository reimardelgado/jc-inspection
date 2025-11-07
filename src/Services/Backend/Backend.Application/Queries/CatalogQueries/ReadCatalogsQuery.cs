using Backend.Application.DTOs.Responses.CatalogResponses;
using Shared.Domain.Specification;

namespace Backend.Application.Queries.CatalogQueries
{
    public class ReadCatalogsQuery : BaseFilter, IRequest<EntityResponse<GetEntitiesResponse<CatalogResponse>>>
    {
        public string? QueryParam { get; set; }

        public ReadCatalogsQuery(string? queryParam, bool loadChildren,
            bool isPagingEnabled, int page, int pageSize)
        {
            QueryParam = queryParam;
            LoadChildren = loadChildren;
            IsPagingEnabled = isPagingEnabled;
            Page = page;
            PageSize = pageSize;
        }
    }
}