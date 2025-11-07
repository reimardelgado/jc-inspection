using Backend.Application.DTOs.Responses.CatalogResponses;
using Backend.Application.Specifications.CatalogSpecs;

namespace Backend.Application.Queries.CatalogQueries
{
    public class ReadCatalogsQueryHandler : IRequestHandler<ReadCatalogsQuery,
        EntityResponse<GetEntitiesResponse<CatalogResponse>>>
    {
        private readonly IRepository<Catalog> _repository;

        public ReadCatalogsQueryHandler(IRepository<Catalog> repository)
        {
            _repository = repository;
        }

        public async Task<EntityResponse<GetEntitiesResponse<CatalogResponse>>> Handle(ReadCatalogsQuery query,
            CancellationToken cancellationToken)
        {
            var spec = new CatalogSpec(query.QueryParam, query.IsPagingEnabled, query.Page, query.PageSize);

            //Get the total amount of entities
            var total = await _repository.CountAsync(spec, cancellationToken);
            //Get entity list
            var entityCollection = await _repository.ListAsync(spec, cancellationToken);

            var filterResponse = new PaginationResponse(query.Page, query.PageSize, total);

            return new GetEntitiesResponse<CatalogResponse>(
                entityCollection.Select(CatalogResponse.FromEntity).ToList(),
                filterResponse
            );
        }
    }
}
