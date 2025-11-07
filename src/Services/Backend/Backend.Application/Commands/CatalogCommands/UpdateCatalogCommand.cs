namespace Backend.Application.Commands.CatalogCommands
{
    public class UpdateCatalogCommand : IRequest<EntityResponse<bool>>
    {
        public Guid CatalogId { get; }
        public string Name { get; set; }

        public UpdateCatalogCommand(Guid catalogId, string name)
        {
            CatalogId = catalogId;
            Name = name;
        }
    }
}