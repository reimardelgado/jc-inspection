
using Backend.Application.DTOs.Responses.InspectionResultResponses;

namespace Backend.Application.Queries.InspectionResultQueries
{
    public class ReadInspectionResultQuery : IRequest<EntityResponse<InspectionResultResponse>>
    {
        public Guid InspectionResultId { get; }

        public ReadInspectionResultQuery(Guid id)
        {
            InspectionResultId = id;
        }
    }
}