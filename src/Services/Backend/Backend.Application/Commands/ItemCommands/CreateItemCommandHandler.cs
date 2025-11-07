namespace Backend.Application.Commands.ItemCommands
{
    public class CreateItemCommandHandler : IRequestHandler<CreateItemCommand, EntityResponse<Guid>>
    {
        
        private readonly IRepository<Item> _repository;

        public CreateItemCommandHandler(IRepository<Item> repository)
        {
            _repository = repository;
        }

        public async Task<EntityResponse<Guid>> Handle(CreateItemCommand command,
            CancellationToken cancellationToken)
        {
            // Repository
            var newItem = CreateItem(command);
            await _repository.AddAsync(newItem, cancellationToken);
            
            return EntityResponse.Success(newItem.Id);
        }

        #region Private Methods

        private Item CreateItem(CreateItemCommand command)
        {
            var newItem = new Item(command.ItemSectionId, command.FormTemplateId, command.Name, command.DataType, command.Value, command.Comment, command.CatalogId);
            return newItem;
        }

        #endregion
    }
}