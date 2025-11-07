namespace Backend.Application.Commands.CatalogValueCommands
{
    public class UpdateCatalogValueCommandHandler : IRequestHandler<UpdateCatalogValueCommand, EntityResponse<bool>>
    {
        private readonly IRepository<CatalogValue> _repository;
        private CatalogValue? _entity;

        public UpdateCatalogValueCommandHandler(IRepository<CatalogValue> repository, CatalogValue? entity)
        {
            _repository = repository;
            _entity = entity;
        }

        public async Task<EntityResponse<bool>> Handle(UpdateCatalogValueCommand command,
            CancellationToken cancellationToken)
        {
            var validateResponse = await Validations(command, cancellationToken);

            if (!validateResponse.IsSuccess)
            {
                return EntityResponse<bool>.Error(validateResponse);
            }

            var dbCatalogValue = await _repository.GetByIdAsync(command.CatalogValueId, cancellationToken);

            if (dbCatalogValue == null)
            {
                return EntityResponse<bool>.Error(validateResponse);
            }

            await UpdateCatalogValue(command, dbCatalogValue, cancellationToken);

            return EntityResponse.Success(true);
        }
        
        #region Private Methods

        private async Task<EntityResponse<bool>> Validations(UpdateCatalogValueCommand command,
            CancellationToken cancellationToken)
        {
            _entity = await _repository.GetByIdAsync(command.CatalogValueId, cancellationToken);
            return _entity is null
                ? EntityResponse<bool>.Error(MessageHandler.CatalogValueNotFound)
                : EntityResponse.Success(true);
        }

        private async Task UpdateCatalogValue(UpdateCatalogValueCommand command, CatalogValue entity,
            CancellationToken cancellationToken)
        {
            entity.Name = command.Name;
            await _repository.UpdateAsync(entity, cancellationToken);
        }

        #endregion
    }
}