using Backend.Domain.SeedWork;

namespace Backend.Domain.Entities;

public class ItemSection : BaseEntity
{
    public string Name { get; set; }
    public Guid FormTemplateId { get; set; }
    public string FormTemplateName { get; set; }
    public string Status { get; set; } = StatusEnum.Active;

    public ItemSection(string name, Guid formTemplateId, string formTemplateName)
    {
        Name = name;
        FormTemplateId = formTemplateId;
        FormTemplateName = formTemplateName;
    }
}