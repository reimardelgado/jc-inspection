namespace Backend.Application.Commands.CatalogValueCommands
{
    public class UpdateCatalogValueCommand : IRequest<EntityResponse<bool>>
    {
        public Guid CatalogValueId { get; }
        public string Name { get; set; }

        public UpdateCatalogValueCommand(Guid catalogValueId, string name)
        {
            CatalogValueId = catalogValueId;
            Name = name;
        }
    }
}