using Backend.Application.DTOs.Responses.ItemResponses;
using Backend.Application.DTOs.Responses.ItemSectionResponses;
using Backend.Application.Queries.ItemQueries;
using Backend.Application.Specifications.ItemSectionSpecs;

namespace Backend.Application.Queries.ItemSectionQueries
{
    public class ReadAllItemSectionsQueryHandler : IRequestHandler<ReadAllItemSectionsQuery,
        EntityResponse<List<ItemSectionModelResponse>>>
    {
        private readonly IRepository<ItemSection> _repository;
        private readonly IMediator _mediator;

        public ReadAllItemSectionsQueryHandler(IRepository<ItemSection> repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }

        public async Task<EntityResponse<List<ItemSectionModelResponse>>> Handle(ReadAllItemSectionsQuery query,
            CancellationToken cancellationToken)
        {
            var spec = new ItemSectionSpec(query.FormTemplateId);

            //Get entity list
            var response = new List<ItemSectionModelResponse>();
            var entityCollection = await _repository.ListAsync(spec, cancellationToken);
            foreach (var itemSection in entityCollection)
            {
                var queryItem = new ReadAllItemsQuery(itemSection.FormTemplateId, itemSection.Id);
                var resultQuery = await _mediator.Send(queryItem, cancellationToken);
                var temp = new ItemSectionModelResponse()
                {
                    Id = itemSection.Id,
                    Name = itemSection.Name,
                    Items = resultQuery.Value,
                    Status = itemSection.Status
                };
                response.Add(temp);
            }

            return response;
        }
    }
}