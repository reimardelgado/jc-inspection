namespace Backend.Application.Commands.InspectionCommands
{
    public class CreateInspectionResultCommandHandler : IRequestHandler<CreateInspectionResultCommand, EntityResponse<Guid>>
    {
        
        private readonly IRepository<InspectionResult> _repository;

        public CreateInspectionResultCommandHandler(IRepository<InspectionResult> repository)
        {
            _repository = repository;
        }
        
        public async Task<EntityResponse<Guid>> Handle(CreateInspectionResultCommand command,
            CancellationToken cancellationToken)
        {
            // Repository
            var newInspection = CreateInspectionResult(command);
            await _repository.AddAsync(newInspection, cancellationToken);
            
            return EntityResponse.Success(newInspection.Id);
        }

        #region Private Methods

        private InspectionResult CreateInspectionResult(CreateInspectionResultCommand command)
        {
            var newInspection = new InspectionResult(command.InspectionId, command.FormTemplateId, command.IdZoho, command.SectionId,
                command.SectionName, command.ItemId, command.ItemName, command.ItemDatatype, command.ItemValue, command.CatalogId,
                command.ItemComment);
            return newInspection;
        }

        #endregion
    }
}