using Backend.Application.DTOs.Responses.ManagerUserResponses;
using Shared.Domain.Specification;

namespace Backend.Application.Queries.ManagerUserQueries
{
    public class ReadManagerUsersQuery : BaseFilter, IRequest<EntityResponse<GetEntitiesResponse<ReadUserResponse>>>
    {
        public string? QueryParam { get; set; }

        public ReadManagerUsersQuery(string? queryParam, bool loadChildren,
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