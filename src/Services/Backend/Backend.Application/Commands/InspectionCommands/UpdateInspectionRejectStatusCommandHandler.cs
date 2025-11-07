using Backend.Application.Commands.NotificationCommands;

namespace Backend.Application.Commands.InspectionCommands
{
    public class UpdateInspectionRejectStatusCommandHandler : IRequestHandler<UpdateInspectionRejectStatusCommand, EntityResponse<bool>>
    {
        #region Constructor & Properties

        private readonly IMediator _mediator;
        private readonly IRepository<Inspection> _repository;
        private readonly IRepository<User> _userRepository;
        private Inspection? _inspection;

        public UpdateInspectionRejectStatusCommandHandler(IMediator mediator, IRepository<Inspection> repository, IRepository<User> userRepository)
        {
            _mediator = mediator;
            _repository = repository;
            _userRepository = userRepository;
        }

        #endregion


        public async Task<EntityResponse<bool>> Handle(UpdateInspectionRejectStatusCommand command,
            CancellationToken cancellationToken)
        {
            var validateResponse = await Validations(command, cancellationToken);

            if (!validateResponse.IsSuccess)
            {
                return EntityResponse<bool>.Error(validateResponse);
            }

            var dbInspection = await _repository.GetByIdAsync(command.InspectionId, cancellationToken);

            if (dbInspection == null)
            {
                return EntityResponse<bool>.Error(validateResponse);
            }

            await UpdateInspectionRejectStatus(command, dbInspection, cancellationToken);

            return EntityResponse.Success(true);
        }

        #region Private Methods

        private async Task<EntityResponse<bool>> Validations(UpdateInspectionRejectStatusCommand command,
            CancellationToken cancellationToken)
        {
            _inspection = await _repository.GetByIdAsync(command.InspectionId, cancellationToken);
            return _inspection is null
                ? EntityResponse<bool>.Error(MessageHandler.InspectionNotFound)
                : EntityResponse.Success(true);
        }

        private async Task UpdateInspectionRejectStatus(UpdateInspectionRejectStatusCommand command, Inspection inspection,
            CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(command.UserId, cancellationToken);
            if(user != null)
            {
                inspection.ReasonRejection = command.ReasonRejection;
                inspection.DateReasonRejection = DateTime.Now;
                inspection.InspectionStatus = InspectionStatusEnum.Rejected;
            }            

            await _repository.UpdateAsync(inspection, cancellationToken);
            
            var userAssigned = await _userRepository.GetByIdAsync(inspection.InspectorId, cancellationToken);
            if (userAssigned != null)
            {
                var response = await _mediator.Send(new SendPushNotificationCommand(userAssigned.MobileId, "Inspection TC",
                    $"Inspection {inspection.Name} has been rejected."), cancellationToken);

                //Send email
                //Send email
                var body =
                    $"Inspection {inspection.Name} has been rejected. <hr>" +
                    "<b>Inspection Data:</b> <br>" +
                    $"<b>Inspection:</b> {inspection.Name} <br>" +
                    $"<b>Deal Number:</b> {inspection.DealNumber} <br>" +
                    $"<b>Address:</b> {inspection.AddressProjectState} {inspection.AddressProjectCity} {inspection.AddressProjectStreet} {inspection.AddressProjectZip} <br>" +
                    $"<b>Reason:</b> {inspection.ReasonRejection} <br>";

                var sendEmailModel = new SendEmailNotificationCommand(userAssigned.Email, "", "", "TuConstruction - Inspection",
                    body,null);
                await _mediator.Send(sendEmailModel, cancellationToken);
            }
        }

        #endregion
    }
}