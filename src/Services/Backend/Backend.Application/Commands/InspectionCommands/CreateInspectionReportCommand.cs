namespace Backend.Application.Commands.InspectionCommands;

public class CreateInspectionReportCommand : IRequest<EntityResponse<byte[]>>
{
    public Guid InspectionId { get; set; }
    public string ContentRootPath { get; set; }

    public CreateInspectionReportCommand(Guid inspectionId, string contentRootPath)
    {
        InspectionId = inspectionId;
        ContentRootPath = contentRootPath;
    }
}
