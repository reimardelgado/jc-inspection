
using Backend.Application.DTOs.Responses.ProfileResponses;
using Backend.Application.Specifications.ProfileSpecs;

namespace Backend.Application.Queries.ProfileQueries
{
    public class ReadAllProfilesQueryHandler : IRequestHandler<ReadAllProfilesQuery,
        EntityResponse<List<ReadProfilesResponse>>>
    {
        private readonly IRepository<Profile> _repository;

        public ReadAllProfilesQueryHandler(IRepository<Profile> repository)
        {
            _repository = repository;
        }

        public async Task<EntityResponse<List<ReadProfilesResponse>>> Handle(ReadAllProfilesQuery query,
            CancellationToken cancellationToken)
        {
            var spec = new ProfileSpec();

            //Get the total amount of entities
            var total = await _repository.CountAsync(spec, cancellationToken);

            //Get entity list
            var entityCollection = await _repository.ListAsync(spec, cancellationToken);

            return entityCollection.Select(ReadProfilesResponse.FromEntity).ToList();
        }
    }
}