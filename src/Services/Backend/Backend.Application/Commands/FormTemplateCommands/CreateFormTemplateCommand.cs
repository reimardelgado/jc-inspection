
using Backend.Application.DTOs;

namespace Backend.Application.Commands.FormTemplateCommands
{
    public class CreateFormTemplateCommand : IRequest<EntityResponse<Guid>>
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string Status { get; set; }
        public List<ItemSectionDto> ItemSectionsDtos { get; set; }

        public CreateFormTemplateCommand(string name, string? description, string status, List<ItemSectionDto> itemSectionsDtos)
        {
            Name = name;
            Description = description;
            Status = status;
            ItemSectionsDtos = itemSectionsDtos;
        }
    }
}