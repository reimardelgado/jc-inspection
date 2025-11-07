
namespace Backend.Application.Commands.CatalogCommands
{
    public class CreateCatalogCommand : IRequest<EntityResponse<Guid>>
    {
        public string Name { get; set; }
        public string Status { get; set; }
        public List<string> CatalogValueNames { get; set; }

        public CreateCatalogCommand(string name, string status, List<string> catalogValueNames)
        {
            Name = name;
            Status = status;
            CatalogValueNames = catalogValueNames;
        }
    }
}