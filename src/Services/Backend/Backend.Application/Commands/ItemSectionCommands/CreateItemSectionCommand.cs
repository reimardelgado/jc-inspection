
namespace Backend.Application.Commands.ItemSectionCommands
{
    public class CreateItemSectionCommand : IRequest<EntityResponse<Guid>>
    {
        public string Name { get; set; }
        public Guid FormTemplateId { get; set; }
        public string FormTemplateName { get; set; }
        public string Status { get; set; }

        public CreateItemSectionCommand(string name, Guid formTemplateId, string formTemplateName, string status)
        {
            Name = name;
            FormTemplateId = formTemplateId;
            FormTemplateName = formTemplateName;
            Status = status;
        }
    }
}