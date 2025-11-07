namespace Backend.Application.Commands.InspectionCommands
{
    public class UpdateInspectionRejectStatusCommand : IRequest<EntityResponse<bool>>
    {
        public Guid InspectionId { get; }
        public Guid UserId { get; set; }
        public string ReasonRejection { get; set; }

        public UpdateInspectionRejectStatusCommand(Guid inspectionId, Guid userId, string reasonRejection)
        {
            InspectionId = inspectionId;
            UserId = userId;
            ReasonRejection = reasonRejection;
        }
    }
}