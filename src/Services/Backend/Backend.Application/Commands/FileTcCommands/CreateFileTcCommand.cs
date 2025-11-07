using Backend.Application.DTOs.Responses.Photos;

namespace Backend.Application.Commands.PhotoCommands
{
    public class CreatePhotoCommand : IRequest<EntityResponse<PhotoResponse>>
    {
        public Guid InspectionId { get; set; }
        public Guid SectionId { get; set; }
        public List<string> FileName { get; set; }
        public List<string> Image { get; }
        public string ContentRootPath { get; set; }

        public CreatePhotoCommand(Guid inspectionId, Guid sectionId, List<string> fileName, List<string> image, string contentRootPath)
        {
            InspectionId = inspectionId;
            SectionId = sectionId;
            FileName = fileName;
            Image = image;
            ContentRootPath = contentRootPath;
        }
    }
}