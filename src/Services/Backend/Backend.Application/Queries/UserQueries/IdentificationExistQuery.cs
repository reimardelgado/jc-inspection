using Backend.Application.DTOs.Responses.UserResponses;

namespace Backend.Application.Queries.UserQueries
{
    public class IdentificationExistQuery : IRequest<EntityResponse<ReadUserMeResponse>>
    {
        public string Identification { get; }


        public IdentificationExistQuery(string identification)
        {
            Identification = identification;
        }
    }
}