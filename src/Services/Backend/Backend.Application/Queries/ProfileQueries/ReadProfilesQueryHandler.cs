using Backend.Application.DTOs.Responses.ProfileResponses;
using Backend.Application.Specifications.ProfileSpecs;

namespace Backend.Application.Queries.ProfileQueries
{
    public class ReadProfilesQueryHandler : IRequestHandler<ReadProfilesQuery, GetEntitiesResponse<ReadProfilesResponse>>
    {
        #region Contructor && Properties

        private readonly IReadRepository<Profile> _repository;

        public ReadProfilesQueryHandler(IReadRepository<Profile> repository)
        {
            _repository = repository;
        }

        #endregion

        public async Task<GetEntitiesResponse<ReadProfilesResponse>> Handle(ReadProfilesQuery query,
            CancellationToken cancellationToken)
        {
            var spec = new ProfileSpec(query.Name, query.Description, query.IsPagingEnabled, query.Page, query.PageSize);

            //Get the total amount of entities
            var total = await _repository.CountAsync(spec, cancellationToken);

            //Get entity list
            var profiles = await _repository.ListAsync(spec, cancellationToken);

            var filterResponse = new PaginationResponse(query.Page, query.PageSize, total);

            return new GetEntitiesResponse<ReadProfilesResponse>(
                profiles.Select(ReadProfilesResponse.FromEntity).ToList(),
                filterResponse
            );
        }
    }
}