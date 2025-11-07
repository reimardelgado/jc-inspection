
using Backend.Application.DTOs.Responses.CatalogValueResponses;

namespace Backend.Application.Queries.CatalogValueQueries
{
    public class ReadCatalogValueQueryHandler : IRequestHandler<ReadCatalogValueQuery,
        EntityResponse<CatalogValueResponse>>
    {
        private readonly IRepository<CatalogValue> _repository;
        private CatalogValue? _entity;

        public ReadCatalogValueQueryHandler(IRepository<CatalogValue> repository)
        {
            _repository = repository;
        }


        public async Task<EntityResponse<CatalogValueResponse>> Handle(ReadCatalogValueQuery query,
            CancellationToken cancellationToken)
        {
            var validateResponse = await Validate(query, cancellationToken);
            if (!validateResponse.IsSuccess)
            {
                return EntityResponse<CatalogValueResponse>.Error(validateResponse);
            }

            var entity = await _repository.GetByIdAsync(query.CatalogValueId, cancellationToken);
            var response = EntityResponse.Success(new CatalogValueResponse(entity!.Id, entity.Name, entity.Status, entity.CatalogId));

            return response;
        }

        private async Task<EntityResponse<bool>> Validate(ReadCatalogValueQuery query,
            CancellationToken cancellationToken)
        {
            _entity = await _repository.GetByIdAsync(query.CatalogValueId, cancellationToken);
            return _entity is null
                ? EntityResponse<bool>.Error(MessageHandler.CatalogValueNotFound)
                : EntityResponse.Success(true);
        }
    }
}