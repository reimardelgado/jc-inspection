using Backend.Application.Queries.PhotoQueries;

namespace Backend.API.DTOs.Requests.FileRequests;

public class ReadAllPhotosRequest
{
    public string InspectionId { get; set; }
    public string SectionId { get; set; }

    public ReadAllPhotosRequest(string inspectionId, string sectionId)
    {
        InspectionId = inspectionId;
        SectionId = sectionId;
    }

    public ReadAllPhotosQuery ToApplicationRequest(string contentRootPath)
    {
        return new ReadAllPhotosQuery(Guid.Parse(InspectionId), Guid.Parse(SectionId), contentRootPath);
    }
}