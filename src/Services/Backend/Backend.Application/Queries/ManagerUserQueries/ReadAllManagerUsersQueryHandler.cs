
using Backend.Application.DTOs.Responses.ManagerUserResponses;
using Backend.Application.Specifications.MemberSpecs;

namespace Backend.Application.Queries.ManagerUserQueries
{
    public class ReadAllManagerUsersQueryHandler : IRequestHandler<ReadAllManagerUsersQuery,
        EntityResponse<List<ReadUsersResponse>>>
    {
        private readonly IRepository<User> _repository;

        public ReadAllManagerUsersQueryHandler(IRepository<User> repository)
        {
            _repository = repository;
        }

        public async Task<EntityResponse<List<ReadUsersResponse>>> Handle(ReadAllManagerUsersQuery query,
            CancellationToken cancellationToken)
        {
            var spec = new ManagerSpec();

            //Get the total amount of entities
            var total = await _repository.CountAsync(spec, cancellationToken);

            //Get entity list
            var entityCollection = await _repository.ListAsync(spec, cancellationToken);

            return entityCollection.Select(ReadUsersResponse.FromEntity).ToList();
        }
    }
}