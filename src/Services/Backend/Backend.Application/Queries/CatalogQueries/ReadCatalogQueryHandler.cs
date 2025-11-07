
using Backend.Application.DTOs.Responses.CatalogResponses;
using Backend.Application.Specifications.CatalogValueSpecs;

namespace Backend.Application.Queries.CatalogQueries
{
    public class ReadCatalogQueryHandler : IRequestHandler<ReadCatalogQuery,
        EntityResponse<CatalogResponse>>
    {
        private readonly IRepository<Catalog> _repository;
        private readonly IRepository<CatalogValue> _repositoryCatalogValue;
        private Catalog? _entity;

        public ReadCatalogQueryHandler(IRepository<Catalog> repository, IRepository<CatalogValue> repositoryCatalogValue)
        {
            _repository = repository;
            _repositoryCatalogValue = repositoryCatalogValue;
        }


        public async Task<EntityResponse<CatalogResponse>> Handle(ReadCatalogQuery query,
            CancellationToken cancellationToken)
        {
            var validateResponse = await Validate(query, cancellationToken);
            if (!validateResponse.IsSuccess)
            {
                return EntityResponse<CatalogResponse>.Error(validateResponse);
            }

            var entity = await _repository.GetByIdAsync(query.CatalogId, cancellationToken);

            var catalogValueSpec = new CatalogValueSpec(query.CatalogId);
            var catalogValues = await _repositoryCatalogValue.ListAsync(catalogValueSpec, cancellationToken);
            var catalogValueDtos = new List<CatalogValueDto>();
            foreach (var item in catalogValues)
            {
                catalogValueDtos.Add(new CatalogValueDto(item.Id, item.Name, item.Status));
            }
            
            var response = EntityResponse.Success(new CatalogResponse(entity!.Id, entity.Name, entity.Status, catalogValueDtos));

            return response;
        }

        private async Task<EntityResponse<bool>> Validate(ReadCatalogQuery query,
            CancellationToken cancellationToken)
        {
            _entity = await _repository.GetByIdAsync(query.CatalogId, cancellationToken);
            return _entity is null
                ? EntityResponse<bool>.Error(MessageHandler.CatalogNotFound)
                : EntityResponse.Success(true);
        }
    }
}