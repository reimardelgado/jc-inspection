using Backend.Application.Configurations;
using Backend.Application.Queries.FormTemplateQueries;
using Backend.Domain.Interfaces.Services;
using Microsoft.Extensions.Options;

namespace Backend.Application.Commands.InspectionCommands
{
    public class SendInspectionToZohoCommandHandler : IRequestHandler<SendInspectionToZohoCommand, EntityResponse<bool>>
    {
        
        private readonly IRepository<Inspection> _repository;
        private readonly IMediator _mediator;
        private readonly IZohoInspectionService _zohoInspectionService;
        private readonly ComunSettings _comunSettings;

        public SendInspectionToZohoCommandHandler(IRepository<Inspection> repository, IMediator mediator,
            IZohoInspectionService zohoInspectionService, IOptions<ComunSettings> comunSettings)
        {
            _repository = repository;
            _mediator = mediator;
            _zohoInspectionService = zohoInspectionService;
            _comunSettings = comunSettings.Value;
        }


        public async Task<EntityResponse<bool>> Handle(SendInspectionToZohoCommand command,
            CancellationToken cancellationToken)
        {
            // Repository
            var entity = await _repository.GetByIdAsync(command.InspectionId, cancellationToken);
            if (entity is null)
            {
                return EntityResponse<bool>.Error(MessageHandler.InspectionNotFound);
            }

            entity.InspectionStatus = InspectionStatusEnum.Completed;
            await _repository.UpdateAsync(entity, cancellationToken);
            //send inspection zoho
            var urlReport = $"{_comunSettings.UrlDomain}{_comunSettings.UrlReport}/{command.InspectionId}/report";
            var res =
                await _zohoInspectionService.SendInspectionDataToZoho(entity.IdZoho, entity.Id.ToString(), urlReport,
                    cancellationToken); 
                
            return EntityResponse.Success(true);
        }

        #region Private Methods

        #endregion
    }
}