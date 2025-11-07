namespace Backend.Application.DTOs.Responses.FormTemplateResponses
{
    public class FormTemplateResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }

        public FormTemplateResponse(Guid id, string name, string description, string status)
        {
            Id = id;
            Name = name;
            Description = description;
            Status = status;
        }

        public static FormTemplateResponse FromEntity(FormTemplate entity)
        {
            return new FormTemplateResponse(entity.Id, entity.Name!, entity.Description!, entity.Status);
        }
    }
    //
    // public record FormTemplateValueDto(Guid Id, string Name, string Status)
    // {
    //     public static FormTemplateValueDto FromEntity(FormTemplateValue catalogValue)
    //     {
    //         return new FormTemplateValueDto(catalogValue.Id, catalogValue.Name, catalogValue.Status);
    //     }
    // }
}