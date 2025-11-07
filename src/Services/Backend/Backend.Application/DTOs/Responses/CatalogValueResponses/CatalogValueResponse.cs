namespace Backend.Application.DTOs.Responses.CatalogValueResponses
{
    public class CatalogValueResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid CatalogId { get; set; }
        public string Status { get; set; }

        public CatalogValueResponse(Guid id, string name, string status, Guid catalogId)
        {
            Id = id;
            Name = name;
            Status = status;
            CatalogId = catalogId;
        }

        public static CatalogValueResponse FromEntity(CatalogValue entity)
        {
            return new CatalogValueResponse(entity.Id, entity.Name, entity.Status,entity.CatalogId);
        }
    }
}