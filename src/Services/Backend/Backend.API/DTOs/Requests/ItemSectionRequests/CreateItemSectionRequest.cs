using Backend.Application.Commands.ItemSectionCommands;
using Backend.Domain.SeedWork;

namespace Backend.API.DTOs.Requests.ItemSectionRequests;

public class CreateItemSectionRequest
{
    public string Name { get; set; }
    public Guid FormTemplateId { get; set; }
    public string FormTemplateName { get; set; }

    public CreateItemSectionRequest(string name, Guid formTemplateId, string formTemplateName)
    {
        Name = name;
        FormTemplateId = formTemplateId;
        FormTemplateName = formTemplateName;
    }

    public CreateItemSectionCommand ToApplicationRequest()
    {
        return new CreateItemSectionCommand(Name, FormTemplateId, FormTemplateName, StatusEnum.Active);
    }
}