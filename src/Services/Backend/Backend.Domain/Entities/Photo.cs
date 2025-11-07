using Backend.Domain.SeedWork;

namespace Backend.Domain.Entities;

public class Photo : BaseEntity
{
    public string Name { get; set; }
    public string FilePath { get; set; }
    public string? ContentType { get; set; }
    public string? Url { get; set; }
    public Guid InspectionId { get; set; }
    public Inspection? Inspection { get; set; }
    public Guid SectionId { get; set; }
    public string Status { get; set; } = StatusEnum.Active;

    public Photo(string name, string filePath, string? contentType, string? url, Guid inspectionId, Guid sectionId)
    {
        Name = name;
        FilePath = filePath;
        ContentType = contentType;
        Url = url;
        InspectionId = inspectionId;
        SectionId = sectionId;
    }
        
    public string GetNameWithExtension()
    {
        var _name = Name.Substring(0, 1).Equals("/") ? Name.Substring(1) : Name;
        return $"{_name}.{ContentTypeSettings.FileToContentTypes[ContentType]}";
    }
}