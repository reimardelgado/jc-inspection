using Backend.Domain.SeedWork;

namespace Backend.Domain.Entities;

public class FormTemplateHistory : BaseEntity
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string Status { get; set; } = StatusEnum.Active;
    public string? IdZoho { get; set; }
    public string? Version { get; set; }
    
    public FormTemplateHistory(string? name, string? description)
    {
        Name = name;
        Description = description;
    }
}