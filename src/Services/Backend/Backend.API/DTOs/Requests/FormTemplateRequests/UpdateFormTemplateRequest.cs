using Backend.Application.Commands.FormTemplateCommands;
using Backend.Application.DTOs;

namespace Backend.API.DTOs.Requests.FormTemplateRequests;

public class UpdateFormTemplateRequest
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public List<ItemSectionDto> ItemSectionsDtos { get; set; }
    
    public UpdateFormTemplateRequest(string name, string? description, List<ItemSectionDto> itemSectionsDtos)
    {
        Name = name;
        Description = description;
        ItemSectionsDtos = itemSectionsDtos;
    }

    public UpdateFormTemplateCommand ToApplicationRequest(Guid id)
    {
        return new UpdateFormTemplateCommand(id, Name, Description, ItemSectionsDtos );
    }
}