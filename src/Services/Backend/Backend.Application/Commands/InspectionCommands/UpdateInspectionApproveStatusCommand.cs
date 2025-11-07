namespace Backend.Application.Commands.InspectionCommands
{
    public class UpdateInspectionApproveStatusCommand : IRequest<EntityResponse<bool>>
    {
        public Guid InspectionId { get; }
        public Guid UserId { get; set; }

        public UpdateInspectionApproveStatusCommand(Guid inspectionId, Guid userId)
        {
            InspectionId = inspectionId;
            UserId = userId;
        }
    }
}