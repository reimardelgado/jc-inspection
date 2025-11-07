namespace Backend.Application.Commands.InspectionCommands
{
    public class DeleteInspectionResultCommand : IRequest<EntityResponse<bool>>
    {
        public Guid InspectionResultId { get; }

        public DeleteInspectionResultCommand(Guid inspectionResultId)
        {
            InspectionResultId = inspectionResultId;
        }
    }
}