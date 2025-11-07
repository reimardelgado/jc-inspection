namespace Backend.Application.Commands.PhotoCommands
{
    public class DeleteFileCommand : IRequest<EntityResponse<bool>>
    {
        public Guid Id { get; set; }
        public string ContentRootPath { get; set; }

        public DeleteFileCommand(Guid id, string contentRootPath)
        {
            Id = id;
            ContentRootPath = contentRootPath;
        }
    }
}