
using Backend.Application.Commands.InspectionCommands;

namespace Backend.API.DTOs.Requests.InspectionResultRequests;

public class DeleteInspectionResultRequest
{
    public DeleteInspectionResultCommand ToApplicationRequest(string id)
    {
        return new DeleteInspectionResultCommand(Guid.Parse(id));
    }
}