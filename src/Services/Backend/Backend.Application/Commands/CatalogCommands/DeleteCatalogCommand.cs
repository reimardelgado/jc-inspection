namespace Backend.Application.Commands.CatalogCommands
{
    public class DeleteCatalogCommand : IRequest<EntityResponse<bool>>
    {
        public Guid CatalogId { get; }

        public DeleteCatalogCommand(Guid catalogId)
        {
            CatalogId = catalogId;
        }
    }
}