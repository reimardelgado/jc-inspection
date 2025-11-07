namespace Backend.Application.Commands.ZohoTokenCommands
{
    public class UpdateZohoTokenCommandHandler : IRequestHandler<UpdateZohoTokenCommand, EntityResponse<bool>>
    {
        private readonly IRepository<ZohoToken> _repository;
        private ZohoToken? _entity;

        public UpdateZohoTokenCommandHandler(IRepository<ZohoToken> repository)
        {
            _repository = repository;
        }

        public async Task<EntityResponse<bool>> Handle(UpdateZohoTokenCommand command,
            CancellationToken cancellationToken)
        {
            var validateResponse = await Validations(command, cancellationToken);

            if (!validateResponse.IsSuccess)
            {
                return EntityResponse<bool>.Error(validateResponse);
            }

            var dbZohoToken = await _repository.GetByIdAsync(command.ZohoTokenId, cancellationToken);

            if (dbZohoToken == null)
            {
                return EntityResponse<bool>.Error(validateResponse);
            }

            await UpdateZohoToken(command, dbZohoToken, cancellationToken);

            return EntityResponse.Success(true);
        }
        
        #region Private Methods

        private async Task<EntityResponse<bool>> Validations(UpdateZohoTokenCommand command,
            CancellationToken cancellationToken)
        {
            _entity = await _repository.GetByIdAsync(command.ZohoTokenId, cancellationToken);
            return _entity is null
                ? EntityResponse<bool>.Error(MessageHandler.ZohoTokenNotFound)
                : EntityResponse.Success(true);
        }

        private async Task UpdateZohoToken(UpdateZohoTokenCommand command, ZohoToken entity,
            CancellationToken cancellationToken)
        {
            entity.AccessToken = command.AccessToken;
            entity.Status = command.Status;
            await _repository.UpdateAsync(entity, cancellationToken);
        }

        #endregion
    }
}