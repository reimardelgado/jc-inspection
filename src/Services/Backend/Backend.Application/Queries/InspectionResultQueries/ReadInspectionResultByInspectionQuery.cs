
using Backend.Application.DTOs.Responses.InspectionResultResponses;

namespace Backend.Application.Queries.InspectionResultQueries
{
    public class ReadInspectionResultByInspectionQuery : IRequest<EntityResponse<List<InspectionResultResponse>>>
    {
        public Guid InspectionId { get; }
        public Guid FormTemplateId { get; }

        public ReadInspectionResultByInspectionQuery(Guid inspectionId, Guid formTemplateId)
        {
            InspectionId = inspectionId;
            FormTemplateId = formTemplateId;
        }
    }
}