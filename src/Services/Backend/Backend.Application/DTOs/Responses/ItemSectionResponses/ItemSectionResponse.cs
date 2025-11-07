namespace Backend.Application.DTOs.Responses.ItemSectionResponses
{
    public class ItemSectionResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid FormTemplateId { get; set; }
        public string FormTemplateName { get; set; }
        public string Status { get; set; }

        public ItemSectionResponse(Guid id, string name, Guid formTemplateId, string formTemplateName, string status)
        {
            Id = id;
            Name = name;
            FormTemplateId = formTemplateId;
            FormTemplateName = formTemplateName; 
            Status = status;
        }

        public static ItemSectionResponse FromEntity(ItemSection entity)
        {
            return new ItemSectionResponse(entity.Id, entity.Name!, entity.FormTemplateId, entity.FormTemplateName, entity.Status);
        }
    }
}