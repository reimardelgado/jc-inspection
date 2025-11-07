using Backend.Application.DTOs.Responses.CatalogValueResponses;
using Backend.Application.Specifications.CatalogValueSpecs;

namespace Backend.Application.Queries.CatalogValueQueries
{
    public class ReadCatalogValuesQueryHandler : IRequestHandler<ReadCatalogValuesQuery,
        EntityResponse<GetEntitiesResponse<CatalogValueResponse>>>
    {
        private readonly IRepository<CatalogValue> _repository;

        public ReadCatalogValuesQueryHandler(IRepository<CatalogValue> repository)
        {
            _repository = repository;
        }

        public async Task<EntityResponse<GetEntitiesResponse<CatalogValueResponse>>> Handle(ReadCatalogValuesQuery query,
            CancellationToken cancellationToken)
        {
            var spec = new CatalogValueSpec(query.CatalogId, query.QueryParam, query.IsPagingEnabled, query.Page, query.PageSize);

            //Get the total amount of entities
            var total = await _repository.CountAsync(spec, cancellationToken);
            //Get entity list
            var entityCollection = await _repository.ListAsync(spec, cancellationToken);

            var filterResponse = new PaginationResponse(query.Page, query.PageSize, total);

            return new GetEntitiesResponse<CatalogValueResponse>(
                entityCollection.Select(CatalogValueResponse.FromEntity).ToList(),
                filterResponse
            );
        }
    }
}
