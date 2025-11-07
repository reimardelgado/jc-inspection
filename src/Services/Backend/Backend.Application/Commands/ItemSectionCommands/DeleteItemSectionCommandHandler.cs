namespace Backend.Application.Commands.ItemSectionCommands
{
    public class DeleteItemSectionCommandHandler : IRequestHandler<DeleteItemSectionCommand, EntityResponse<bool>>
    {
        private readonly IRepository<ItemSection> _repository;

        public DeleteItemSectionCommandHandler(IRepository<ItemSection> repository)
        {
            _repository = repository;
        }

        public async Task<EntityResponse<bool>> Handle(DeleteItemSectionCommand command, CancellationToken cancellationToken)
        {
            var validateResponse = await Validations(command, cancellationToken);
            if (!validateResponse.IsSuccess)
            {
                return EntityResponse<bool>.Error(validateResponse);
            }

            var dbItemSection = validateResponse.Value;
            await UpdateItemSection(dbItemSection!, cancellationToken);

            return EntityResponse.Success(true);
        }
        
        #region Private Methods

        private async Task<EntityResponse<ItemSection>> Validations(DeleteItemSectionCommand command,
            CancellationToken cancellationToken)
        {
            var area = await _repository.GetByIdAsync(command.ItemSectionId, cancellationToken);

            return area is null
                ? EntityResponse<ItemSection>.Error(MessageHandler.ItemSectionNotFound)
                : EntityResponse.Success(area);
        }

        private async Task UpdateItemSection(ItemSection entity, CancellationToken cancellationToken)
        {
            entity.Status = StatusEnum.Deleted;
            await _repository.UpdateAsync(entity, cancellationToken);
        }

        #endregion
    }
}