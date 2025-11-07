namespace Backend.Application.Commands.ItemCommands
{
    public class DeleteItemCommandHandler : IRequestHandler<DeleteItemCommand, EntityResponse<bool>>
    {
        private readonly IRepository<Item> _repository;

        public DeleteItemCommandHandler(IRepository<Item> repository)
        {
            _repository = repository;
        }

        public async Task<EntityResponse<bool>> Handle(DeleteItemCommand command, CancellationToken cancellationToken)
        {
            var validateResponse = await Validations(command, cancellationToken);
            if (!validateResponse.IsSuccess)
            {
                return EntityResponse<bool>.Error(validateResponse);
            }

            var dbItem = validateResponse.Value;
            await UpdateItem(dbItem!, cancellationToken);

            return EntityResponse.Success(true);
        }
        
        #region Private Methods

        private async Task<EntityResponse<Item>> Validations(DeleteItemCommand command,
            CancellationToken cancellationToken)
        {
            var area = await _repository.GetByIdAsync(command.ItemId, cancellationToken);

            return area is null
                ? EntityResponse<Item>.Error(MessageHandler.ItemNotFound)
                : EntityResponse.Success(area);
        }

        private async Task UpdateItem(Item entity, CancellationToken cancellationToken)
        {
            entity.Status = StatusEnum.Deleted;
            await _repository.UpdateAsync(entity, cancellationToken);
        }

        #endregion
    }
}