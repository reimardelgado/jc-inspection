namespace Backend.Application.DTOs.Responses.CatalogResponses
{
    public class CatalogResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public List<CatalogValueDto> CatalogValueDtos { get; set; }

        public CatalogResponse(Guid id, string name, string status, List<CatalogValueDto> catalogValueDtos)
        {
            Id = id;
            Name = name;
            Status = status;
            CatalogValueDtos = catalogValueDtos;
        }

        public static CatalogResponse FromEntity(Catalog entity)
        {
            return new CatalogResponse(entity.Id, entity.Name, entity.Status, new List<CatalogValueDto>());
        }
    }
    
    public record CatalogValueDto(Guid Id, string Name, string Status)
    {
        public static CatalogValueDto FromEntity(CatalogValue catalogValue)
        {
            return new CatalogValueDto(catalogValue.Id, catalogValue.Name, catalogValue.Status);
        }
    }
}