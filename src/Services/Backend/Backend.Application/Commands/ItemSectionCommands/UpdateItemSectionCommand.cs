namespace Backend.Application.Commands.ItemSectionCommands
{
    public class UpdateItemSectionCommand : IRequest<EntityResponse<bool>>
    {
        public Guid ItemSectionId { get; }
        public string Name { get; set; }
        public Guid FormTemplateId { get; set; }

        public UpdateItemSectionCommand(Guid itemSectionId, string name, Guid formTemplateId)
        {
            ItemSectionId = itemSectionId;
            Name = name;
            FormTemplateId = formTemplateId;
        }
    }
}