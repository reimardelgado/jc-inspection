using Backend.Application.Commands.NotificationCommands;
using Backend.Application.Queries.FormTemplateQueries;

namespace Backend.Application.Commands.InspectionCommands
{
    public class CreateInspectionCommandHandler : IRequestHandler<CreateInspectionCommand, EntityResponse<Guid>>
    {
        
        private readonly IRepository<Inspection> _repository;
        private readonly IRepository<InspectionResult> _inspectionResultRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IMediator _mediator;

        public CreateInspectionCommandHandler(IRepository<Inspection> repository, IMediator mediator, IRepository<InspectionResult> inspectionResultRepository, IRepository<User> userRepository)
        {
            _repository = repository;
            _mediator = mediator;
            _inspectionResultRepository = inspectionResultRepository;
            _userRepository = userRepository;
        }
        
        public async Task<EntityResponse<Guid>> Handle(CreateInspectionCommand command,
            CancellationToken cancellationToken)
        {
            // Repository
            var newInspection = CreateInspection(command);
            await _repository.AddAsync(newInspection, cancellationToken);
            
            //create inspection result
            await CreateInspectionResult(newInspection, cancellationToken);
            
                //notifications
                var userAssigned = await _userRepository.GetByIdAsync(command.InspectorId, cancellationToken);
                if (userAssigned != null)
                {
                    await _mediator.Send(new SendPushNotificationCommand(userAssigned.MobileId, "Inspection TC",
                        $"Inspection {command.Name} has been assigned."), cancellationToken);

                    //Send email
                    var sendEmailModel = new SendEmailNotificationCommand(userAssigned.Email, "", "", "TuConstruction - Inspection",
                        $"Inspection {command.Name} has been assigned.", new List<string>());
                    await _mediator.Send(sendEmailModel, cancellationToken);
                }
            
            return EntityResponse.Success(newInspection.Id);
        }

        #region Private Methods

        private Inspection CreateInspection(CreateInspectionCommand command)
        {
            var newInspection = new Inspection(command.Name, command.DealNumber, command.InspectorId, command.ZohoOwnerId,
                command.ZohoOwnerName, command.InspectionDate, command.FormTemplateId, command.AddressProjectCity,
                command.AddressProjectState, command.AddressProjectStreet, command.AddressProjectZip, String.Empty, 
                null);
            return newInspection;
        }

        private async Task<bool> CreateInspectionResult(Inspection inspection, CancellationToken cancellationToken)
        {
            var templateQuery = new ReadFormTemplateQuery(inspection.FormTemplateId);
            var templateResponse = await _mediator.Send(templateQuery, cancellationToken);
            var template = templateResponse.Value;
            if (template is null)
            {
                return EntityResponse<bool>.Error(MessageHandler.InspectionResultNotFound);
            }
            
            foreach (var aux in template.Sections)
            {
                foreach (var it in aux.Items)
                {
                    var inspectionResultEntity = new InspectionResult(inspection.Id, inspection.FormTemplateId,
                        inspection.IdZoho, aux.Id, aux.Name, it.Id, it.Name, 
                        it.DataType, it.Value, it.CatalogId, it.Comment);
                    await _inspectionResultRepository.AddAsync(inspectionResultEntity, cancellationToken);
                }
            }
            
            return EntityResponse.Success(true);
        }

        #endregion
    }
}