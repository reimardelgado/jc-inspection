using Backend.Application.Commands.PhotoCommands;

namespace Backend.API.DTOs.Requests.FileRequests
{
    public class DeleteFileRequest
    {
        private string Id { get; }
        
        public DeleteFileCommand ToApplicationRequest(string contentRootPath)
        {
            return new DeleteFileCommand(Guid.Parse(Id), contentRootPath);
        }
    }
}