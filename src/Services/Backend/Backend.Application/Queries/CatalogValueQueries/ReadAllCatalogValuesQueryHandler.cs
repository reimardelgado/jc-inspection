using Backend.Application.DTOs.Responses.CatalogValueResponses;
using Backend.Application.Specifications.CatalogValueSpecs;

namespace Backend.Application.Queries.CatalogValueQueries
{
    public class ReadAllCatalogValuesQueryHandler : IRequestHandler<ReadAllCatalogValuesQuery,
        EntityResponse<List<CatalogValueResponse>>>
    {
        private readonly IRepository<CatalogValue> _repository;

        public ReadAllCatalogValuesQueryHandler(IRepository<CatalogValue> repository)
        {
            _repository = repository;
        }

        public async Task<EntityResponse<List<CatalogValueResponse>>> Handle(ReadAllCatalogValuesQuery query,
            CancellationToken cancellationToken)
        {
            var spec = new CatalogValueSpec();

            //Get the total amount of entities
            var total = await _repository.CountAsync(spec, cancellationToken);

            //Get entity list
            var entityCollection = await _repository.ListAsync(spec, cancellationToken);

            return entityCollection.Select(CatalogValueResponse.FromEntity).ToList();
        }
    }
}