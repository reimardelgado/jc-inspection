using System.Runtime;
using Backend.Domain.SeedWork;

namespace Backend.Domain.Entities;

public class CatalogValue : BaseEntity
{
    public string Name { get; set; }
    public string Status { get; set; } = StatusEnum.Active;

    public Guid CatalogId { get; set; }
    public Catalog? Catalog { get; set; }

    public CatalogValue(string name, Guid catalogId)
    {
        Name = name;
        CatalogId = catalogId;
    }
}