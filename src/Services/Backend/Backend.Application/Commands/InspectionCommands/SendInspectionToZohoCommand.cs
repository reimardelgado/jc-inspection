
namespace Backend.Application.Commands.InspectionCommands
{
    public class SendInspectionToZohoCommand : IRequest<EntityResponse<bool>>
    {
        public Guid InspectionId { get; set; }

        public SendInspectionToZohoCommand(Guid inspectionId)
        {
            InspectionId = inspectionId;
        }
    }
}