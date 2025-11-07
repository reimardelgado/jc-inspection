namespace Backend.Application.Commands.CatalogCommands
{
    public class CreateCatalogCommandHandler : IRequestHandler<CreateCatalogCommand, EntityResponse<Guid>>
    {
        
        private readonly IRepository<Catalog> _repository;
        private readonly IRepository<CatalogValue> _repositoryCatalogValue;

        public CreateCatalogCommandHandler(IRepository<Catalog> repository, IRepository<CatalogValue> repositoryCatalogValue)
        {
            _repository = repository;
            _repositoryCatalogValue = repositoryCatalogValue;
        }
        
        public async Task<EntityResponse<Guid>> Handle(CreateCatalogCommand command,
            CancellationToken cancellationToken)
        {
            // Repository
            var newCatalog = CreateCatalog(command);
            await _repository.AddAsync(newCatalog, cancellationToken);

            foreach (var optionValue in command.CatalogValueNames)
            {
                var newCatalogValue = new CatalogValue(optionValue, newCatalog.Id);
                await _repositoryCatalogValue.AddAsync(newCatalogValue, cancellationToken);
            }
            
            return EntityResponse.Success(newCatalog.Id);
        }

        #region Private Methods

        private Catalog CreateCatalog(CreateCatalogCommand command)
        {
            var newCatalog = new Catalog(command.Name);
            return newCatalog;
        }

        #endregion
    }
}