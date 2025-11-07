using Backend.Application.DTOs.Responses.ManagerUserResponses;
using Backend.Application.Specifications.MemberSpecs;

namespace Backend.Application.Queries.ManagerUserQueries
{
    public class ReadManagerUserQueryQueryHandler : IRequestHandler<ReadManagerUserQuery, EntityResponse<ReadUserResponse>>
    {
        private readonly IReadRepository<User> _repository;

        public ReadManagerUserQueryQueryHandler(IReadRepository<User> repository)
        {
            _repository = repository;
        }

        public async Task<EntityResponse<ReadUserResponse>> Handle(ReadManagerUserQuery query,
            CancellationToken cancellationToken)
        {
            var spec = new ManagerSpec(query.UserId);
            var user = await _repository.GetBySpecAsync(spec, cancellationToken);
            if (user is null)
            {
                return EntityResponse<ReadUserResponse>.Error(
                    EntityResponseUtils.GenerateMsg(MessageHandler.UserNotFound));
            }

            return ReadUserResponse.FromEntity(user);
        }
    }
}