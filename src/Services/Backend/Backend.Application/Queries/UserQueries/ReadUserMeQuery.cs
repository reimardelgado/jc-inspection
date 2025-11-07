using Backend.Application.DTOs.Responses.UserResponses;

namespace Backend.Application.Queries.UserQueries
{
    public class ReadUserMeQuery : IRequest<EntityResponse<ReadUserMeResponse>>
    {
        public Guid UserId { get; }
        public string Token { get; set; }

        public ReadUserMeQuery(Guid userId, string token)
        {
            UserId = userId;
            Token = token;
        }
    }
}