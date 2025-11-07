using Backend.Application.DTOs.Responses.ItemResponses;
using Backend.Application.Specifications.ItemSpecs;

namespace Backend.Application.Queries.ItemQueries
{
    public class ReadAllItemsQueryHandler : IRequestHandler<ReadAllItemsQuery,
        EntityResponse<List<ItemModelResponse>>>
    {
        private readonly IRepository<Item> _repository;

        public ReadAllItemsQueryHandler(IRepository<Item> repository)
        {
            _repository = repository;
        }

        public async Task<EntityResponse<List<ItemModelResponse>>> Handle(ReadAllItemsQuery query,
            CancellationToken cancellationToken)
        {
            var spec = new ItemSpec(query.FormTemplateId, query.ItemSectionId);

            //Get entity list
            var response = new List<ItemModelResponse>();
            var entityCollection = await _repository.ListAsync(spec, cancellationToken);
            foreach (var item in entityCollection)
            {
                var temp = new ItemModelResponse(item.Id, item.Name, item.DataType, item.Value, item.Comment, item.Status);
                temp.CatalogId = item.CatalogId;
                temp.CatalogName = item.Catalog is not null ? item.Catalog.Name : string.Empty;
                response.Add(temp);
            }

            return response;
        }
    }
}