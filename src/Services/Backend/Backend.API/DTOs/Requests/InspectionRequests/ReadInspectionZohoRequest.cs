using Backend.Application.Queries.InspectionQueries;
using Shared.API.DTOs;

namespace Backend.API.DTOs.Requests.InspectionRequests;

public class ReadInspectionZohoRequest
{
    private Guid InspectionId { get; }

    public ReadInspectionZohoRequest(Guid inspectionId)
    {
        InspectionId = inspectionId;
    }

    public ReadInspectionZohoQuery ToApplicationRequest()
    {
        return new ReadInspectionZohoQuery(InspectionId);
    }
}