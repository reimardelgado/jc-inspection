namespace Backend.Application.Commands.CatalogValueCommands
{
    public class DeleteCatalogValueCommand : IRequest<EntityResponse<bool>>
    {
        public Guid CatalogValueId { get; }

        public DeleteCatalogValueCommand(Guid catalogValueId)
        {
            CatalogValueId = catalogValueId;
        }
    }
}