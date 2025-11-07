namespace Backend.Application.Commands.ItemSectionCommands
{
    public class CreateItemSectionCommandHandler : IRequestHandler<CreateItemSectionCommand, EntityResponse<Guid>>
    {
        
        private readonly IRepository<ItemSection> _repository;

        public CreateItemSectionCommandHandler(IRepository<ItemSection> repository)
        {
            _repository = repository;
        }

        public async Task<EntityResponse<Guid>> Handle(CreateItemSectionCommand command,
            CancellationToken cancellationToken)
        {
            // Repository
            var newItemSection = CreateItemSection(command);
            await _repository.AddAsync(newItemSection, cancellationToken);
            
            return EntityResponse.Success(newItemSection.Id);
        }

        #region Private Methods

        private ItemSection CreateItemSection(CreateItemSectionCommand command)
        {
            var newItemSection = new ItemSection(command.Name, command.FormTemplateId, command.FormTemplateName);
            return newItemSection;
        }

        #endregion
    }
}