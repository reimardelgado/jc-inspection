using Backend.Application.DTOs.Responses.InspectionResponses;
using Shared.Domain.Specification;

namespace Backend.Application.Queries.InspectionQueries
{
    public class ReadInspectionsQuery : BaseFilter, IRequest<EntityResponse<GetEntitiesResponse<InspectionResponse>>>
    {
        public string? QueryParam { get; set; }

        public ReadInspectionsQuery(string? queryParam, bool loadChildren,
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