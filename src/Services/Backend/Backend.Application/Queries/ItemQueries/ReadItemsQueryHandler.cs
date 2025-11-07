using Backend.Application.DTOs.Responses.ItemResponses;
using Backend.Application.Specifications.ItemSpecs;

namespace Backend.Application.Queries.ItemQueries
{
    public class ReadItemsQueryHandler : IRequestHandler<ReadItemsQuery,
        EntityResponse<GetEntitiesResponse<ItemResponse>>>
    {
        private readonly IRepository<Item> _repository;

        public ReadItemsQueryHandler(IRepository<Item> repository)
        {
            _repository = repository;
        }

        public async Task<EntityResponse<GetEntitiesResponse<ItemResponse>>> Handle(ReadItemsQuery query,
            CancellationToken cancellationToken)
        {
            var spec = new ItemSpec(query.FormTemplateId, query.QueryParam, query.IsPagingEnabled, query.Page, query.PageSize);

            //Get the total amount of entities
            var total = await _repository.CountAsync(spec, cancellationToken);
            //Get entity list
            var entityCollection = await _repository.ListAsync(spec, cancellationToken);

            var filterResponse = new PaginationResponse(query.Page, query.PageSize, total);

            return new GetEntitiesResponse<ItemResponse>(
                entityCollection.Select(ItemResponse.FromEntity).ToList(),
                filterResponse
            );
        }
    }
}
