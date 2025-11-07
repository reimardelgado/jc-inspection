using Backend.Application.DTOs.Responses.InspectionResponses;
using Backend.Domain.DTOs.Requests.Zoho;
using Backend.Domain.Interfaces.Services;

namespace Backend.Application.Queries.InspectionQueries
{
    public class ReadInspectionZohoQueryHandler : IRequestHandler<ReadInspectionZohoQuery,
        EntityResponse<ZohoInspection>>
    {
        private readonly IRepository<Inspection> _repository;
        private readonly IZohoInspectionService _zohoInspectionService;
        private Inspection? _entity;

        public ReadInspectionZohoQueryHandler(IRepository<Inspection> repository, IZohoInspectionService zohoInspectionService)
        {
            _repository = repository;
            _zohoInspectionService = zohoInspectionService;
        }


        public async Task<EntityResponse<ZohoInspection>> Handle(ReadInspectionZohoQuery query,
            CancellationToken cancellationToken)
        {
            var validateResponse = await Validate(query, cancellationToken);
            if (!validateResponse.IsSuccess)
            {
                return EntityResponse<ZohoInspection>.Error(validateResponse);
            }

            var entity = await _repository.GetByIdAsync(query.InspectionId, cancellationToken);
            
            //Inspection Zoho
            var inspectionZoho = await _zohoInspectionService.ReadInspectionZoho(entity.IdZoho, entity.Id.ToString(), cancellationToken); 

            //var response = EntityResponse.Success(InspectionResponse.FromEntity(entity!));

            return inspectionZoho;
        }

        private async Task<EntityResponse<bool>> Validate(ReadInspectionZohoQuery query,
            CancellationToken cancellationToken)
        {
            _entity = await _repository.GetByIdAsync(query.InspectionId, cancellationToken);
            return _entity is null
                ? EntityResponse<bool>.Error(MessageHandler.InspectionNotFound)
                : EntityResponse.Success(true);
        }
    }
}