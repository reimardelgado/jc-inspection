
using Backend.Application.Commands.InspectionCommands;

namespace Backend.API.DTOs.Requests.InspectionRequests;

public class DeleteInspectionRequest
{
    public DeleteInspectionCommand ToApplicationRequest(string id)
    {
        return new DeleteInspectionCommand(Guid.Parse(id));
    }
}