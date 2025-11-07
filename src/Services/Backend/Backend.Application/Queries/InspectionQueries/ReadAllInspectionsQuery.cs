
using Backend.Application.DTOs.Responses.InspectionResponses;

namespace Backend.Application.Queries.InspectionQueries
{
    public class ReadAllInspectionsQuery : IRequest<EntityResponse<List<InspectionResponse>>>
    {
        public Guid UserId { get; set; }
        public ReadAllInspectionsQuery(Guid userId)
        {
            UserId = userId;
        }
    }
}