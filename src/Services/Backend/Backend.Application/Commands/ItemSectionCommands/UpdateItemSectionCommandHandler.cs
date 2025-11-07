namespace Backend.Application.Commands.ItemSectionCommands
{
    public class UpdateItemSectionCommandHandler : IRequestHandler<UpdateItemSectionCommand, EntityResponse<bool>>
    {
        private readonly IRepository<ItemSection> _repository;
        private ItemSection? _entity;

        public UpdateItemSectionCommandHandler(IRepository<ItemSection> repository, ItemSection? entity)
        {
            _repository = repository;
            _entity = entity;
        }

        public async Task<EntityResponse<bool>> Handle(UpdateItemSectionCommand command,
            CancellationToken cancellationToken)
        {
            var validateResponse = await Validations(command, cancellationToken);

            if (!validateResponse.IsSuccess)
            {
                return EntityResponse<bool>.Error(validateResponse);
            }

            var dbItemSection = await _repository.GetByIdAsync(command.ItemSectionId, cancellationToken);

            if (dbItemSection == null)
            {
                return EntityResponse<bool>.Error(validateResponse);
            }

            await UpdateItemSection(command, dbItemSection, cancellationToken);

            return EntityResponse.Success(true);
        }
        
        #region Private Methods

        private async Task<EntityResponse<bool>> Validations(UpdateItemSectionCommand command,
            CancellationToken cancellationToken)
        {
            _entity = await _repository.GetByIdAsync(command.ItemSectionId, cancellationToken);
            return _entity is null
                ? EntityResponse<bool>.Error(MessageHandler.ItemSectionNotFound)
                : EntityResponse.Success(true);
        }

        private async Task UpdateItemSection(UpdateItemSectionCommand command, ItemSection entity,
            CancellationToken cancellationToken)
        {
            entity.Name = command.Name;
            entity.FormTemplateId = command.FormTemplateId;
            await _repository.UpdateAsync(entity, cancellationToken);
        }

        #endregion
    }
}