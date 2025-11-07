
using Backend.Application.DTOs.Responses.InspectionResponses;
using Backend.Domain.DTOs.Requests.Zoho;

namespace Backend.Application.Queries.InspectionQueries
{
    public class ReadInspectionZohoQuery : IRequest<EntityResponse<ZohoInspection>>
    {
        public Guid InspectionId { get; }

        public ReadInspectionZohoQuery(Guid id)
        {
            InspectionId = id;
        }
    }
}