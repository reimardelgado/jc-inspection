using Backend.Application.Specifications.PhotoSpecs;
using Backend.Domain.Interfaces;

namespace Backend.Application.Commands.PhotoCommands
{
    public class DeleteFileCommandHandler : IRequestHandler<DeleteFileCommand, EntityResponse<bool>>
    {
        #region Constructor & properties

        private readonly ILogger<DeleteFileCommandHandler> _logger;
        private readonly IRepository<Photo> _fileRepository;

        public DeleteFileCommandHandler(ILogger<DeleteFileCommandHandler> logger, IRepository<Photo> fileRepository)
        {
            _logger = logger;
            _fileRepository = fileRepository;
        }

        #endregion
        
        #region Public Methods
        public async Task<EntityResponse<bool>> Handle(DeleteFileCommand command, CancellationToken cancellationToken)
        {
            var validateResponse = await Validations(command, cancellationToken);
            if (!validateResponse.IsSuccess)
            {
                return EntityResponse<bool>.Error(validateResponse);
            }

            var entity = validateResponse.Value;
            
            entity.Status = StatusEnum.Deleted;
            await _fileRepository.UpdateAsync(entity, cancellationToken);

            return EntityResponse.Success(true);
        }

        #endregion
        
        private async Task<EntityResponse<Photo>> Validations(DeleteFileCommand command,
            CancellationToken cancellationToken)
        {
            var spec = new PhotoSpec(command.Id);
            var photo = await _fileRepository.GetBySpecAsync(spec, cancellationToken);

            return photo is null
                ? EntityResponse<Photo>.Error(MessageHandler.FileNotExistMsg)
                : EntityResponse.Success(photo);
        }
    }
}