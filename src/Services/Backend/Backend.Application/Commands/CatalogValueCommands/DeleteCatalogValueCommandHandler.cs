namespace Backend.Application.Commands.CatalogValueCommands
{
    public class DeleteCatalogValueCommandHandler : IRequestHandler<DeleteCatalogValueCommand, EntityResponse<bool>>
    {
        private readonly IMediator _mediator;
        private readonly IRepository<CatalogValue> _repository;

        public DeleteCatalogValueCommandHandler(IMediator mediator, IRepository<CatalogValue> repository)
        {
            _mediator = mediator;
            _repository = repository;
        }

        public async Task<EntityResponse<bool>> Handle(DeleteCatalogValueCommand command, CancellationToken cancellationToken)
        {
            var validateResponse = await Validations(command, cancellationToken);
            if (!validateResponse.IsSuccess)
            {
                return EntityResponse<bool>.Error(validateResponse);
            }

            var dbCatalogValue = validateResponse.Value;
            await UpdateCatalogValue(dbCatalogValue!, cancellationToken);

            return EntityResponse.Success(true);
        }
        
        #region Private Methods

        private async Task<EntityResponse<CatalogValue>> Validations(DeleteCatalogValueCommand command,
            CancellationToken cancellationToken)
        {
            var area = await _repository.GetByIdAsync(command.CatalogValueId, cancellationToken);

            return area is null
                ? EntityResponse<CatalogValue>.Error(MessageHandler.CatalogValueNotFound)
                : EntityResponse.Success(area);
        }

        private async Task UpdateCatalogValue(CatalogValue entity, CancellationToken cancellationToken)
        {
            entity.Status = StatusEnum.Deleted;
            await _repository.UpdateAsync(entity, cancellationToken);
        }

        #endregion
    }
}