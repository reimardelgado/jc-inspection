namespace Backend.Application.DTOs.Responses.Photos
{
    public class PhotoResponse
    {
        public Guid Id { get; set; }
        public Guid Inspection { get; set; }
        public Guid SectionId { get; set; }
        public List<string> Image { get; set; }
        public List<string> Name { get; set; }
        public string Status { get; set; }


        public PhotoResponse(Guid id, Guid inspection, Guid sectionId, List<string> image, List<string> name, string status)
        {
            Id = id;
            Inspection = inspection;
            SectionId = sectionId;
            Image = image;
            Name = name;
            Status = status;
        }

    }
    
}