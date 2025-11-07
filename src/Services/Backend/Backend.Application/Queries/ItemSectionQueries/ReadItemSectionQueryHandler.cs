using Backend.Application.DTOs.Responses.ItemSectionResponses;

namespace Backend.Application.Queries.ItemSectionQueries
{
    public class ReadItemSectionQueryHandler : IRequestHandler<ReadItemSectionQuery,
        EntityResponse<ItemSectionResponse>>
    {
        private readonly IRepository<ItemSection> _repository;
        private ItemSection? _entity;

        public ReadItemSectionQueryHandler(IRepository<ItemSection> repository)
        {
            _repository = repository;
        }


        public async Task<EntityResponse<ItemSectionResponse>> Handle(ReadItemSectionQuery query,
            CancellationToken cancellationToken)
        {
            var validateResponse = await Validate(query, cancellationToken);
            if (!validateResponse.IsSuccess)
            {
                return EntityResponse<ItemSectionResponse>.Error(validateResponse);
            }

            var entity = await _repository.GetByIdAsync(query.ItemSectionId, cancellationToken);
            
            var response = EntityResponse.Success(new ItemSectionResponse(entity!.Id, entity.Name!, entity.FormTemplateId!, entity.FormTemplateName, entity.Status));

            return response;
        }

        private async Task<EntityResponse<bool>> Validate(ReadItemSectionQuery query,
            CancellationToken cancellationToken)
        {
            _entity = await _repository.GetByIdAsync(query.ItemSectionId, cancellationToken);
            return _entity is null
                ? EntityResponse<bool>.Error(MessageHandler.ItemSectionNotFound)
                : EntityResponse.Success(true);
        }
    }
}