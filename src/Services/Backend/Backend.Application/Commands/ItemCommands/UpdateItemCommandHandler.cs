namespace Backend.Application.Commands.ItemCommands
{
    public class UpdateItemCommandHandler : IRequestHandler<UpdateItemCommand, EntityResponse<bool>>
    {
        private readonly IRepository<Item> _repository;
        private Item? _entity;

        public UpdateItemCommandHandler(IRepository<Item> repository, Item? entity)
        {
            _repository = repository;
            _entity = entity;
        }

        public async Task<EntityResponse<bool>> Handle(UpdateItemCommand command,
            CancellationToken cancellationToken)
        {
            var validateResponse = await Validations(command, cancellationToken);

            if (!validateResponse.IsSuccess)
            {
                return EntityResponse<bool>.Error(validateResponse);
            }

            var dbItem = await _repository.GetByIdAsync(command.ItemId, cancellationToken);

            if (dbItem == null)
            {
                return EntityResponse<bool>.Error(validateResponse);
            }

            await UpdateItem(command, dbItem, cancellationToken);

            return EntityResponse.Success(true);
        }
        
        #region Private Methods

        private async Task<EntityResponse<bool>> Validations(UpdateItemCommand command,
            CancellationToken cancellationToken)
        {
            _entity = await _repository.GetByIdAsync(command.ItemId, cancellationToken);
            return _entity is null
                ? EntityResponse<bool>.Error(MessageHandler.ItemNotFound)
                : EntityResponse.Success(true);
        }

        private async Task UpdateItem(UpdateItemCommand command, Item entity,
            CancellationToken cancellationToken)
        {
            entity.ItemSectionId = command.ItemSectionId;
            entity.FormTemplateId = command.FormTemplateId;
            entity.Name = command.Name;
            entity.DataType = command.DataType;
            entity.Value = command.Value;
            entity.Comment = command.Value;
            entity.CatalogId = command.CatalogId;
            await _repository.UpdateAsync(entity, cancellationToken);
        }

        #endregion
    }
}