
using Backend.Application.DTOs.Responses.Photos;

namespace Backend.Application.Queries.PhotoQueries
{
    public class ReadAllPhotosQuery : IRequest<EntityResponse<PhotoResponse>>
    {

        public Guid InspectionId { get; set; }
        public Guid SectionId { get; set; }
        public string ContentRootPath { get; set; }

        public ReadAllPhotosQuery(Guid inspectionId, Guid sectionId, string contentRootPath)
        {
            InspectionId = inspectionId;
            SectionId = sectionId;
            ContentRootPath = contentRootPath;
        }
    }
}