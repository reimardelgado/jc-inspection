namespace Backend.Application.Commands.ItemCommands
{
    public class DeleteItemCommand : IRequest<EntityResponse<bool>>
    {
        public Guid ItemId { get; }

        public DeleteItemCommand(Guid itemIdId)
        {
            ItemId = itemIdId;
        }
    }
}