using Backend.Application.DTOs;

namespace Backend.Application.Commands.FormTemplateCommands
{
    public class UpdateFormTemplateCommand : IRequest<EntityResponse<Guid>>
    {
        public Guid FormTemplateId { get; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public List<ItemSectionDto> ItemSectionsDtos { get; set; }

        public UpdateFormTemplateCommand(Guid formTemplateId, string name, string? description, List<ItemSectionDto> itemSectionsDtos)
        {
            FormTemplateId = formTemplateId;
            Name = name;
            Description = description;
            ItemSectionsDtos = itemSectionsDtos;
        }
    }
}