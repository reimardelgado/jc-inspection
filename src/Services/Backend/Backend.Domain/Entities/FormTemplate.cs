using Backend.Domain.SeedWork;

namespace Backend.Domain.Entities;

public class FormTemplate : BaseEntity
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string Status { get; set; } = StatusEnum.Active;
    public string? IdZoho { get; set; }
    public string? Version { get; set; }
    
    public FormTemplate(string? name, string? description)
    {
        Name = name;
        Description = description;
    }
}