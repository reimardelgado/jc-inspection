using Backend.Application.Commands.PhotoCommands;

namespace Backend.API.DTOs.Requests.FileRequests
{
    public class CreateFileRequest
    {
        public List<string> FileName { get; set; }
        public List<string> Image { get; set; }
        public string InspectionId { get; set; }
        public string SectionId { get; set; }

        public CreateFileRequest(List<string> fileName, List<string> image, string inspectionId, string sectionId)
        {
            FileName = fileName;
            Image = image;
            InspectionId = inspectionId;
            SectionId = sectionId;
        }

        public CreatePhotoCommand ToApplicationRequest(string contentRootPath)
        {
            return new CreatePhotoCommand(Guid.Parse(InspectionId), Guid.Parse(SectionId), FileName, Image,
                contentRootPath);
        }
        
        public List<IFormFile> Base64ToImage()
        {
            List<IFormFile> formFiles = new List<IFormFile>();
            for (int i = 0; i < Image.Count; i++)
            {
                byte[] bytes = Convert.FromBase64String(Image.ElementAt(i));
                MemoryStream stream = new MemoryStream(bytes);
        
                IFormFile file = new FormFile(stream, 0, bytes.Length, FileName.ElementAt(i), FileName.ElementAt(i));
                formFiles.Add(file);
            }
            return formFiles;
        } 
    }
}