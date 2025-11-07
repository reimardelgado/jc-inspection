using Backend.Application.Commands.FormTemplateCommands;
using Backend.Application.DTOs;
using Backend.Domain.Entities;
using Backend.Domain.SeedWork;

namespace Backend.API.DTOs.Requests.FormTemplateRequests;

public class CreateFormTemplateRequest
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public List<ItemSectionDto> ItemSectionsDtos { get; set; }

    public CreateFormTemplateRequest(string name, string? description, List<ItemSectionDto> itemSectionsDtos)
    {
        Name = name;
        Description = description;
        ItemSectionsDtos = itemSectionsDtos;
    }

    public CreateFormTemplateCommand ToApplicationRequest()
    {
        return new CreateFormTemplateCommand(Name, Description, StatusEnum.Active, ItemSectionsDtos);
    }
}