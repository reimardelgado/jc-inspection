using Backend.Application.Queries.InspectionQueries;
using Shared.API.DTOs;

namespace Backend.API.DTOs.Requests.InspectionRequests;

public class ReadInspectionRequest : BaseFilterDto
{
    private Guid InspectionId { get; }

    public ReadInspectionRequest(Guid inspectionId)
    {
        InspectionId = inspectionId;
    }

    public ReadInspectionQuery ToApplicationRequest()
    {
        return new ReadInspectionQuery(InspectionId);
    }
}