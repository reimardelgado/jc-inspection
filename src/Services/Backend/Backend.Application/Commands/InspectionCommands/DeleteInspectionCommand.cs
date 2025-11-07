namespace Backend.Application.Commands.InspectionCommands
{
    public class DeleteInspectionCommand : IRequest<EntityResponse<bool>>
    {
        public Guid InspectionId { get; }

        public DeleteInspectionCommand(Guid inspectionId)
        {
            InspectionId = inspectionId;
        }
    }
}