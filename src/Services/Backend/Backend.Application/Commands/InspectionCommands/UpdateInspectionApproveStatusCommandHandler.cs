using Backend.Application.Commands.NotificationCommands;

namespace Backend.Application.Commands.InspectionCommands
{
    public class UpdateInspectionApproveStatusCommandHandler : IRequestHandler<UpdateInspectionApproveStatusCommand, EntityResponse<bool>>
    {
        #region Constructor & Properties

        private readonly IMediator _mediator;
        private readonly IRepository<Inspection> _inspectionRepository;
        private readonly IRepository<User> _userRepository;
        private Inspection? _inspection;

        public UpdateInspectionApproveStatusCommandHandler(IMediator mediator, IRepository<Inspection> inspectionRepository,
            IRepository<User> userRepository)
        {
            _mediator = mediator;
            _inspectionRepository = inspectionRepository;
            _userRepository = userRepository;
        }

        #endregion


        public async Task<EntityResponse<bool>> Handle(UpdateInspectionApproveStatusCommand command,
            CancellationToken cancellationToken)
        {
            var validateResponse = await Validations(command, cancellationToken);

            if (!validateResponse.IsSuccess)
            {
                return EntityResponse<bool>.Error(validateResponse);
            }

            var dbInspection = await _inspectionRepository.GetByIdAsync(command.InspectionId, cancellationToken);

            if (dbInspection == null)
            {
                return EntityResponse<bool>.Error(validateResponse);
            }

            await UpdateInspectionApproveStatus(command, dbInspection, cancellationToken);

            return EntityResponse.Success(true);
        }

        #region Private Methods

        private async Task<EntityResponse<bool>> Validations(UpdateInspectionApproveStatusCommand command,
            CancellationToken cancellationToken)
        {
            _inspection = await _inspectionRepository.GetByIdAsync(command.InspectionId, cancellationToken);
            return _inspection is null
                ? EntityResponse<bool>.Error(MessageHandler.InspectionNotFound)
                : EntityResponse.Success(true);
        }

        private async Task UpdateInspectionApproveStatus(UpdateInspectionApproveStatusCommand command, Inspection inspection,
            CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(command.UserId, cancellationToken);
            if(user != null)
            {
                inspection.InspectionStatus = InspectionStatusEnum.Approved;
            }            

            await _inspectionRepository.UpdateAsync(inspection, cancellationToken);

            var userAssigned = await _userRepository.GetByIdAsync(inspection.InspectorId, cancellationToken);
            if (userAssigned != null)
            {
                await _mediator.Send(new SendPushNotificationCommand(userAssigned.MobileId, "Inspection TC",
                    $"Inspection {inspection.Name} has been approved."), cancellationToken);

                //Send email
                var sendEmailModel = new SendEmailNotificationCommand(userAssigned.Email, "", "", "TuConstruction - Inspection",
                    $"Inspection {inspection.Name} has been approved.", new List<string>());
                await _mediator.Send(sendEmailModel, cancellationToken);
            }
        }

        #endregion
    }
}