using Backend.Application.DTOs.Responses.UserResponses;
using Backend.Application.Specifications.MemberSpecs;

namespace Backend.Application.Queries.UserQueries
{
    public class IdentificationExistQueryHandler : IRequestHandler<IdentificationExistQuery, EntityResponse<ReadUserMeResponse>>
    {
        private readonly IReadRepository<User> _userRepository;

        public IdentificationExistQueryHandler(IReadRepository<User> userRepository)
        {
            _userRepository = userRepository;

        }

        public async Task<EntityResponse<ReadUserMeResponse>> Handle(IdentificationExistQuery query, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetBySpecAsync(
                new UserSpec(query.Identification), cancellationToken);

            //if (user == null)
            //    return EntityResponse<ReadUserMeResponse>.Error(
            //        EntityResponseUtils.GenerateMsg(MessageHandler.IdentificationNotFound));
            //var response = EntityResponse.Success(new ReadUserMeResponse(user.Id, user.Username,
            //    user.FirstName, user.LastName, user.Email, user.FullName, new List<NotificationsConfig>(),
            //    user.Gender, user.DateOfBirth, user.MaritalStatus, user.Status, user.Phone, user.IdentificationType,
            //    user.Identification, user.Latitud, user.Longitud, new List<string>(), user.AreaId));

            return null;

        }
    }
}