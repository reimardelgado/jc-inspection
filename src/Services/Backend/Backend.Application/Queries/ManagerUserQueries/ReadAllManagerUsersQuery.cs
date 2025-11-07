
using Backend.Application.DTOs.Responses.ManagerUserResponses;

namespace Backend.Application.Queries.ManagerUserQueries
{
    public class ReadAllManagerUsersQuery : IRequest<EntityResponse<List<ReadUsersResponse>>>
    {


        public ReadAllManagerUsersQuery()
        {

        }
    }
}