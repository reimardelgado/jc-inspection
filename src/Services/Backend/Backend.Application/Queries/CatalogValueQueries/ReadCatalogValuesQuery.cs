using Backend.Application.DTOs.Responses.CatalogValueResponses;
using Shared.Domain.Specification;

namespace Backend.Application.Queries.CatalogValueQueries
{
    public class ReadCatalogValuesQuery : BaseFilter, IRequest<EntityResponse<GetEntitiesResponse<CatalogValueResponse>>>
    {
        public Guid? CatalogId { get; set; }
        public string? QueryParam { get; set; }

        public ReadCatalogValuesQuery(Guid? catalogId, string? queryParam, bool loadChildren,
            bool isPagingEnabled, int page, int pageSize)
        {
            QueryParam = queryParam;
            CatalogId = catalogId;
            LoadChildren = loadChildren;
            IsPagingEnabled = isPagingEnabled;
            Page = page;
            PageSize = pageSize;
        }
    }
}