using Backend.Application.DTOs.Responses.CatalogResponses;
using Backend.Application.Specifications.CatalogSpecs;

namespace Backend.Application.Queries.CatalogQueries
{
    public class ReadAllCatalogsQueryHandler : IRequestHandler<ReadAllCatalogsQuery,
        EntityResponse<List<CatalogResponse>>>
    {
        private readonly IRepository<Catalog> _repository;

        public ReadAllCatalogsQueryHandler(IRepository<Catalog> repository)
        {
            _repository = repository;
        }

        public async Task<EntityResponse<List<CatalogResponse>>> Handle(ReadAllCatalogsQuery query,
            CancellationToken cancellationToken)
        {
            var spec = new CatalogSpec();

            //Get the total amount of entities
            var total = await _repository.CountAsync(spec, cancellationToken);

            //Get entity list
            var entityCollection = await _repository.ListAsync(spec, cancellationToken);

            return entityCollection.Select(CatalogResponse.FromEntity).ToList();
        }
    }
}