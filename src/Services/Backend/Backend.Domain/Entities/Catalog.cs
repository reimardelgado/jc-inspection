using Backend.Domain.SeedWork;

namespace Backend.Domain.Entities;

public class Catalog : BaseEntity
{
    public string Name { get; set; }
    public string Status { get; set; } = StatusEnum.Active;

    public Catalog(string name)
    {
        Name = name;
    }
}