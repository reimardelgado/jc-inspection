using Backend.Application.DTOs.Responses.ItemResponses;

namespace Backend.Application.Queries.ItemQueries
{
    public class ReadItemQueryHandler : IRequestHandler<ReadItemQuery,
        EntityResponse<ItemResponse>>
    {
        private readonly IRepository<Item> _repository;
        private Item? _entity;

        public ReadItemQueryHandler(IRepository<Item> repository)
        {
            _repository = repository;
        }


        public async Task<EntityResponse<ItemResponse>> Handle(ReadItemQuery query,
            CancellationToken cancellationToken)
        {
            var validateResponse = await Validate(query, cancellationToken);
            if (!validateResponse.IsSuccess)
            {
                return EntityResponse<ItemResponse>.Error(validateResponse);
            }

            var entity = await _repository.GetByIdAsync(query.ItemId, cancellationToken);
            
            var response = EntityResponse.Success(new ItemResponse(entity!.Id, entity.ItemSectionId, entity.ItemSection!.Name,
                entity.FormTemplateId, entity.FormTemplate!.Name!, entity.Name!, entity.DataType, entity.Value, entity.Comment,
                entity.CatalogId, entity.Catalog!.Name, entity.Status));

            return response;
        }

        private async Task<EntityResponse<bool>> Validate(ReadItemQuery query,
            CancellationToken cancellationToken)
        {
            _entity = await _repository.GetByIdAsync(query.ItemId, cancellationToken);
            return _entity is null
                ? EntityResponse<bool>.Error(MessageHandler.ItemNotFound)
                : EntityResponse.Success(true);
        }
    }
}