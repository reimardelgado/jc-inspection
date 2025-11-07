namespace Backend.Application.Commands.InspectionCommands
{
    public class UpdateInspectionCommandHandler : IRequestHandler<UpdateInspectionCommand, EntityResponse<bool>>
    {
        private readonly IRepository<Inspection> _repository;
        private Inspection? _entity;

        public UpdateInspectionCommandHandler(IRepository<Inspection> repository)
        {
            _repository = repository;
        }

        public async Task<EntityResponse<bool>> Handle(UpdateInspectionCommand command,
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

            await UpdateInspection(command, dbInspection, cancellationToken);

            return EntityResponse.Success(true);
        }
        
        #region Private Methods

        private async Task<EntityResponse<bool>> Validations(UpdateInspectionCommand command,
            CancellationToken cancellationToken)
        {
            _entity = await _repository.GetByIdAsync(command.InspectionId, cancellationToken);
            return _entity is null
                ? EntityResponse<bool>.Error(MessageHandler.InspectionNotFound)
                : EntityResponse.Success(true);
        }

        private async Task UpdateInspection(UpdateInspectionCommand command, Inspection entity,
            CancellationToken cancellationToken)
        {
            entity.Name = command.Name;
            entity.DealNumber = command.DealNumber;
            entity.InspectorId = command.InspectionId;
            entity.ZohoOwnerId = command.ZohoOwnerId;
            entity.ZohoOwnerName = command.ZohoOwnerName;
            entity.InspectionDate = command.InspectionDate;
            entity.FormTemplateId = command.FormTemplateId;
            entity.AddressProjectCity = command.AddressProjectCity;
            entity.AddressProjectState = command.AddressProjectState;
            entity.AddressProjectStreet = command.AddressProjectStreet;
            entity.AddressProjectZip = command.AddressProjectZip;
            await _repository.UpdateAsync(entity, cancellationToken);
        }

        #endregion
    }
}