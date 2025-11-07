namespace Backend.Application.Commands.ItemSectionCommands
{
    public class DeleteItemSectionCommand : IRequest<EntityResponse<bool>>
    {
        public Guid ItemSectionId { get; }

        public DeleteItemSectionCommand(Guid itemSectionIdId)
        {
            ItemSectionId = itemSectionIdId;
        }
    }
}