using Backend.Application.DTOs.Responses.ProfileResponses;

namespace Backend.Application.Queries.ProfileQueries
{
    public class ReadProfileQuery : IRequest<EntityResponse<ReadProfileResponse>>
    {
        public Guid ProfileId { get; }

        public ReadProfileQuery(Guid profileId)
        {
            ProfileId = profileId;
        }
    }
}