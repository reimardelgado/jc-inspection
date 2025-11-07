
using Backend.Application.DTOs.Responses.InspectionResultResponses;

namespace Backend.Application.Queries.InspectionResultQueries
{
    public class ReadAllInspectionResultsQuery : IRequest<EntityResponse<List<InspectionResultResponse>>>
    {
        public ReadAllInspectionResultsQuery()
        {
        }
    }
}