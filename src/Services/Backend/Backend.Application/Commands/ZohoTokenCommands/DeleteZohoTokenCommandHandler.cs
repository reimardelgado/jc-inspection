namespace Backend.Application.Commands.ZohoTokenCommands
{
    public class DeleteZohoTokenCommandHandler : IRequestHandler<DeleteZohoTokenCommand, EntityResponse<bool>>
    {
        private readonly IMediator _mediator;
        private readonly IRepository<ZohoToken> _repository;

        public DeleteZohoTokenCommandHandler(IMediator mediator, IRepository<ZohoToken> repository)
        {
            _mediator = mediator;
            _repository = repository;
        }

        public async Task<EntityResponse<bool>> Handle(DeleteZohoTokenCommand command, CancellationToken cancellationToken)
        {
            var validateResponse = await Validations(command, cancellationToken);
            if (!validateResponse.IsSuccess)
            {
                return EntityResponse<bool>.Error(validateResponse);
            }

            var dbZohoToken = validateResponse;
            await UpdateZohoToken(dbZohoToken!, cancellationToken);

            return EntityResponse.Success(true);
        }
        
        #region Private Methods

        private async Task<EntityResponse<ZohoToken>> Validations(DeleteZohoTokenCommand command,
            CancellationToken cancellationToken)
        {
            var area = await _repository.GetByIdAsync(command.ZohoTokenId, cancellationToken);

            return area is null
                ? EntityResponse<ZohoToken>.Error(MessageHandler.ZohoTokenNotFound)
                : EntityResponse.Success(area);
        }

        private async Task UpdateZohoToken(ZohoToken entity, CancellationToken cancellationToken)
        {
            entity.Status = StatusEnum.Deleted;
            await _repository.UpdateAsync(entity, cancellationToken);
        }

        #endregion
    }
}