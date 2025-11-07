using Backend.Application.DTOs.Responses.ProfileResponses;

namespace Backend.Application.Queries.ProfileQueries
{
    public class ReadPermissionQuery : IRequest<EntityResponse<List<PermissionResponse>>>
    {
        public ReadPermissionQuery()
        {
        }
    }
}