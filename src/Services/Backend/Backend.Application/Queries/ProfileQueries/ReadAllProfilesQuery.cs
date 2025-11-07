using Backend.Application.DTOs.Responses.ProfileResponses;

namespace Backend.Application.Queries.ProfileQueries
{
    public class ReadAllProfilesQuery : IRequest<EntityResponse<List<ReadProfilesResponse>>>
    {


        public ReadAllProfilesQuery()
        {

        }
    }
}