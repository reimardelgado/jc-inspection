using Backend.Application.Commands.InspectionCommands;
using Backend.Application.Queries.InspectionQueries;
using Shared.API.DTOs;

namespace Backend.API.DTOs.Requests.InspectionRequests;

public class SendInspectionZohoRequest
{
    private string InspectionId { get; }

    public SendInspectionZohoRequest(string inspectionId)
    {
        InspectionId = inspectionId;
    }

    public SendInspectionToZohoCommand ToApplicationRequest()
    {
        return new SendInspectionToZohoCommand(Guid.Parse(InspectionId));
    }
}