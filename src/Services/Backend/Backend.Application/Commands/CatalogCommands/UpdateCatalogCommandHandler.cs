namespace Backend.Application.Commands.CatalogCommands
{
    public class UpdateCatalogCommandHandler : IRequestHandler<UpdateCatalogCommand, EntityResponse<bool>>
    {
        private readonly IRepository<Catalog> _repository;
        private Catalog? _entity;

        public UpdateCatalogCommandHandler(IRepository<Catalog> repository)
        {
            _repository = repository;
        }

        public async Task<EntityResponse<bool>> Handle(UpdateCatalogCommand command,
            CancellationToken cancellationToken)
        {
            var validateResponse = await Validations(command, cancellationToken);

            if (!validateResponse.IsSuccess)
            {
                return EntityResponse<bool>.Error(validateResponse);
            }

            var dbCatalog = await _repository.GetByIdAsync(command.CatalogId, cancellationToken);

            if (dbCatalog == null)
            {
                return EntityResponse<bool>.Error(validateResponse);
            }

            await UpdateCatalog(command, dbCatalog, cancellationToken);

            return EntityResponse.Success(true);
        }
        
        #region Private Methods

        private async Task<EntityResponse<bool>> Validations(UpdateCatalogCommand command,
            CancellationToken cancellationToken)
        {
            _entity = await _repository.GetByIdAsync(command.CatalogId, cancellationToken);
            return _entity is null
                ? EntityResponse<bool>.Error(MessageHandler.CatalogNotFound)
                : EntityResponse.Success(true);
        }

        private async Task UpdateCatalog(UpdateCatalogCommand command, Catalog entity,
            CancellationToken cancellationToken)
        {
            entity.Name = command.Name;
            await _repository.UpdateAsync(entity, cancellationToken);
        }

        #endregion
    }
}