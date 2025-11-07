
using Backend.Application.DTOs.Responses.InspectionResponses;

namespace Backend.Application.Queries.InspectionQueries
{
    public class ReadInspectionQuery : IRequest<EntityResponse<InspectionResponse>>
    {
        public Guid InspectionId { get; }

        public ReadInspectionQuery(Guid id)
        {
            InspectionId = id;
        }
    }
}