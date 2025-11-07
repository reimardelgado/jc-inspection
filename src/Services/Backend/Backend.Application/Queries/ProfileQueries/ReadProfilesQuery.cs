using Backend.Application.DTOs.Responses.ProfileResponses;

namespace Backend.Application.Queries.ProfileQueries
{
    public class ReadProfilesQuery : IRequest<GetEntitiesResponse<ReadProfilesResponse>>
    {
        public string? Name { get; }
        public string? Description { get; }
        public bool LoadChildren { get; }
        public bool IsPagingEnabled { get; }
        public int Page { get; }
        public int PageSize { get; }

        public ReadProfilesQuery(string? name, string? description, bool loadChildren, bool isPagingEnabled, int page, int pageSize)
        {
            Name = name;
            Description = description;
            LoadChildren = loadChildren;
            IsPagingEnabled = isPagingEnabled;
            Page = page;
            PageSize = pageSize;
        }
    }
}