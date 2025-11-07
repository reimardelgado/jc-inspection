namespace Backend.Application.Commands.ZohoTokenCommands
{
    public class CreateZohoTokenCommandHandler : IRequestHandler<CreateZohoTokenCommand, EntityResponse<Guid>>
    {
        
        private readonly IRepository<ZohoToken> _repository;

        public CreateZohoTokenCommandHandler(IRepository<ZohoToken> repository)
        {
            _repository = repository;
        }
        
        public async Task<EntityResponse<Guid>> Handle(CreateZohoTokenCommand command,
            CancellationToken cancellationToken)
        {
            // Repository
            var newZohoToken = CreateZohoToken(command);
            await _repository.AddAsync(newZohoToken, cancellationToken);
            
            return EntityResponse.Success(newZohoToken.Id);
        }

        #region Private Methods

        private ZohoToken CreateZohoToken(CreateZohoTokenCommand command)
        {
            var newZohoToken = new ZohoToken(command.AccessToken);
            return newZohoToken;
        }

        #endregion
    }
}