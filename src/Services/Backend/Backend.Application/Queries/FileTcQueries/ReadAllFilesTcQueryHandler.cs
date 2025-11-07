using Backend.Application.DTOs.Responses.Photos;
using Backend.Application.Specifications.PhotoSpecs;

namespace Backend.Application.Queries.PhotoQueries
{
    public class ReadAllPhotosQueryHandler : IRequestHandler<ReadAllPhotosQuery,
        EntityResponse<PhotoResponse>>
    {
        private readonly IRepository<Photo> _repository;

        public ReadAllPhotosQueryHandler(IRepository<Photo> repository)
        {
            _repository = repository;
        }

        public async Task<EntityResponse<PhotoResponse>> Handle(ReadAllPhotosQuery query,
            CancellationToken cancellationToken)
        {
            var spec = new PhotoSpec(query.InspectionId, query.SectionId);
            //Get entity list
            var entityCollection = await _repository.ListAsync(spec, cancellationToken);
            if (entityCollection.Any())
            {
                var photo = entityCollection.FirstOrDefault();
                var id = photo.Id;
                var status = photo != null ? photo.Status : "";
                var names = new List<string>();
                var images = new List<string>();
                foreach (var entity in entityCollection)
                {
                    names.Add(entity.Name);
                    images.Add(GetImage(entity.FilePath, query.ContentRootPath));
                }

                var photoResponse = new PhotoResponse(id, query.InspectionId, query.SectionId, images, names, status);
                return photoResponse;
            }

            return null;
        }

        #region Private Methods

        public string GetImage(string pathFile, string contentRootPath)
        {
            try
            {
                var base64 = string.Empty;
                var dir = $"{contentRootPath}/AppFiles";
                dir = Path.Combine(dir, pathFile);

                if (File.Exists(dir))
                {
                    var arrayByte = File.ReadAllBytes(dir);
                    if (arrayByte != null)
                    {
                        base64 = Convert.ToBase64String(arrayByte);
                    }
                }

                return base64;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        #endregion
    }
}