namespace Backend.Application.Commands.CatalogValueCommands
{
    public class CreateCatalogValueCommandHandler : IRequestHandler<CreateCatalogValueCommand, EntityResponse<Guid>>
    {
        
        private readonly IRepository<CatalogValue> _repository;

        public CreateCatalogValueCommandHandler(IRepository<CatalogValue> repository)
        {
            _repository = repository;
        }
        
        public async Task<EntityResponse<Guid>> Handle(CreateCatalogValueCommand command,
            CancellationToken cancellationToken)
        {
            // Repository
            var newCatalogValue = CreateCatalogValue(command);
            await _repository.AddAsync(newCatalogValue, cancellationToken);
            return EntityResponse.Success(newCatalogValue.Id);
        }

        #region Private Methods

        private CatalogValue CreateCatalogValue(CreateCatalogValueCommand command)
        {
            var newCatalogValue = new CatalogValue(command.Name, command.CatalogId);
            return newCatalogValue;
        }

        #endregion
    }
}