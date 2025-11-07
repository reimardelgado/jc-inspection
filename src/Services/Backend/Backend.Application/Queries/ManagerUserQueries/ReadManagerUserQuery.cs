using Backend.Application.DTOs.Responses.ManagerUserResponses;

namespace Backend.Application.Queries.ManagerUserQueries
{
    public class ReadManagerUserQuery : IRequest<EntityResponse<ReadUserResponse>>
    {
        public Guid UserId { get; }

        public ReadManagerUserQuery(Guid userId)
        {
            UserId = userId;
        }
    }
}