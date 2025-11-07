namespace Backend.Application.Commands.CatalogCommands
{
    public class DeleteCatalogCommandHandler : IRequestHandler<DeleteCatalogCommand, EntityResponse<bool>>
    {
        private readonly IMediator _mediator;
        private readonly IRepository<Catalog> _repository;

        public DeleteCatalogCommandHandler(IMediator mediator, IRepository<Catalog> repository)
        {
            _mediator = mediator;
            _repository = repository;
        }

        public async Task<EntityResponse<bool>> Handle(DeleteCatalogCommand command, CancellationToken cancellationToken)
        {
            var validateResponse = await Validations(command, cancellationToken);
            if (!validateResponse.IsSuccess)
            {
                return EntityResponse<bool>.Error(validateResponse);
            }

            var dbCatalog = validateResponse.Value;
            await UpdateCatalog(dbCatalog!, cancellationToken);

            return EntityResponse.Success(true);
        }
        
        #region Private Methods

        private async Task<EntityResponse<Catalog>> Validations(DeleteCatalogCommand command,
            CancellationToken cancellationToken)
        {
            var catalog = await _repository.GetByIdAsync(command.CatalogId, cancellationToken);

            return catalog is null
                ? EntityResponse<Catalog>.Error(MessageHandler.CatalogNotFound)
                : EntityResponse.Success(catalog);
        }

        private async Task UpdateCatalog(Catalog entity, CancellationToken cancellationToken)
        {
            entity.Status = StatusEnum.Deleted;
            await _repository.UpdateAsync(entity, cancellationToken);
        }

        #endregion
    }
}