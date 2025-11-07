using Backend.Application.Configurations;
using Backend.Application.DTOs.Responses.Photos;
using Backend.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Backend.Application.Commands.PhotoCommands
{
    public class CreatePhotoCommandHandler : IRequestHandler<CreatePhotoCommand, EntityResponse<PhotoResponse>>
    {
        #region Constructor & properties

        private readonly ILogger<CreatePhotoCommandHandler> _logger;
        private readonly IRepository<Photo> _repository;
        private readonly IRepository<Inspection> _inspectionRepository;
        private readonly IConfiguration _configuration;
        private Photo? entity;

        public CreatePhotoCommandHandler(ILogger<CreatePhotoCommandHandler> logger, IRepository<Photo> repository, 
            IConfiguration configuration, IRepository<Inspection> inspectionRepository)
        {
            _logger = logger;
            _repository = repository;
            _configuration = configuration;
            _inspectionRepository = inspectionRepository;
        }

        #endregion
        
        #region Public Methods
        public async Task<EntityResponse<PhotoResponse>> Handle(CreatePhotoCommand command, CancellationToken cancellationToken)
        {

            var inspection = await _inspectionRepository.GetByIdAsync(command.InspectionId, cancellationToken);
            if (inspection is null)
            {
                return EntityResponse<PhotoResponse>.Error(MessageHandler.InspectionNotFound);
            }

            for (int i = 0; i < command.Image.Count; i++)
            {
                byte[] bytes = Convert.FromBase64String(command.Image.ElementAt(i));
                var stream = new MemoryStream(bytes);
                var imagePath =
                    await SaveInspectionImageAsync(inspection.DealNumber, stream, command.ContentRootPath, cancellationToken);

                var fileNameSplit = imagePath.Split("/");
                var fileName = fileNameSplit.ElementAt(fileNameSplit.Length - 1);
                entity = new Photo(fileName, imagePath, string.Empty, string.Empty,
                    command.InspectionId, command.SectionId);
            
                await _repository.AddAsync(entity, cancellationToken);
            }
            var fileResponse = new PhotoResponse(entity.Id, entity.InspectionId, entity.SectionId, command.Image, command.FileName, entity.Status);
            return EntityResponse.Success(fileResponse);
        }
        #endregion
        
        #region Private Method
        public async Task<string> SaveInspectionImageAsync(string dealNumber, Stream fileStream, string contentRootPath,
            CancellationToken cancellationToken = default)
        {
            var fileName = $"img-{dealNumber}{DateTime.Now.Ticks}.jpg";
            var path = $"inspections/images/{dealNumber}";

            await SaveFile(fileStream, fileName, contentRootPath,path,  cancellationToken);
            return $"{path}/{fileName}";
        }

        public string GetSavePath(string fileName, string prefix, string contentRootPath)
        {
            var dir = $"{contentRootPath}/AppFiles";
            if (prefix != null)
                dir = Path.Combine(dir, prefix);

            return Path.Combine(dir, fileName);
        }

        private async Task SaveFile(Stream fileStream, string fileName, string contentRootPath, string prefix = default,
            CancellationToken cancellationToken = default)
        {
            var savePath = GetSavePath(fileName, prefix, contentRootPath);
            var dir = $"{contentRootPath}/AppFiles";
            if (prefix != null)
                dir = Path.Combine(dir, prefix);

            var exist = Directory.Exists(dir);
            if (!exist)
            {
                Directory.CreateDirectory(dir);
            }

            await using var stream = File.Create(savePath);
            await fileStream.CopyToAsync(stream, cancellationToken);
        }
        #endregion
    }
}