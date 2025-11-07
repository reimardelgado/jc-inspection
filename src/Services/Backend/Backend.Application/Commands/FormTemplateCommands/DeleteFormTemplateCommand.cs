namespace Backend.Application.Commands.FormTemplateCommands
{
    public class DeleteFormTemplateCommand : IRequest<EntityResponse<bool>>
    {
        public Guid FormTemplateId { get; }

        public DeleteFormTemplateCommand(Guid formTemplateId)
        {
            FormTemplateId = formTemplateId;
        }
    }
}