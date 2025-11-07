using Backend.Application.DTOs.Responses.InspectionResultResponses;
using Shared.Domain.Specification;

namespace Backend.Application.Queries.InspectionResultQueries
{
    public class ReadInspectionResultsQuery : BaseFilter, IRequest<EntityResponse<GetEntitiesResponse<InspectionResultResponse>>>
    {
        public string? QueryParam { get; set; }

        public ReadInspectionResultsQuery(string? queryParam, bool loadChildren,
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