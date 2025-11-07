using Backend.Application.DTOs.Responses.ItemSectionResponses;
using Backend.Application.Specifications.ItemSectionSpecs;

namespace Backend.Application.Queries.ItemSectionQueries
{
    public class ReadItemSectionsQueryHandler : IRequestHandler<ReadItemSectionsQuery,
        EntityResponse<GetEntitiesResponse<ItemSectionResponse>>>
    {
        private readonly IRepository<ItemSection> _repository;

        public ReadItemSectionsQueryHandler(IRepository<ItemSection> repository)
        {
            _repository = repository;
        }

        public async Task<EntityResponse<GetEntitiesResponse<ItemSectionResponse>>> Handle(ReadItemSectionsQuery query,
            CancellationToken cancellationToken)
        {
            var spec = new ItemSectionSpec(query.FormTemplateId, query.QueryParam, query.IsPagingEnabled, query.Page, query.PageSize);

            //Get the total amount of entities
            var total = await _repository.CountAsync(spec, cancellationToken);
            //Get entity list
            var entityCollection = await _repository.ListAsync(spec, cancellationToken);

            var filterResponse = new PaginationResponse(query.Page, query.PageSize, total);

            return new GetEntitiesResponse<ItemSectionResponse>(
                entityCollection.Select(ItemSectionResponse.FromEntity).ToList(),
                filterResponse
            );
        }
    }
}
