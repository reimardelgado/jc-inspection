
namespace Backend.Application.Commands.CatalogValueCommands
{
    public class CreateCatalogValueCommand : IRequest<EntityResponse<Guid>>
    {
        public string Name { get; set; }
        public string Status { get; set; }
        public Guid CatalogId { get; set; }

        public CreateCatalogValueCommand(string name, string status, Guid catalogId)
        {
            Name = name;
            Status = status;
            CatalogId = catalogId;
        }
    }
}