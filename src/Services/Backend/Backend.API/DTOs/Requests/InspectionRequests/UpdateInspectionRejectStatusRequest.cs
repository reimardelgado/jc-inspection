using Backend.Application.Commands.InspectionCommands;

namespace Backend.API.DTOs.Requests.InspectionRequests;

public class UpdateInspectionRejectStatusRequest
{
    public string ReasonRejection { get; set; }

    public UpdateInspectionRejectStatusRequest(string reasonRejection)
    {
        ReasonRejection = reasonRejection;
    }

    public UpdateInspectionRejectStatusCommand ToApplicationRequest(Guid inspectionId, Guid userId)
    {
        return new UpdateInspectionRejectStatusCommand(inspectionId, userId, ReasonRejection);
    }
}