using Backend.Application.Commands.ItemSectionCommands;

namespace Backend.API.DTOs.Requests.ItemSectionRequests;

public class UpdateItemSectionRequest
{
    public string Name { get; set; }
    public Guid FormTemplateId { get; set; }

    public UpdateItemSectionRequest(string name, Guid formTemplateId)
    {
        Name = name;
        FormTemplateId = formTemplateId;
    }

    public UpdateItemSectionCommand ToApplicationRequest(Guid id)
    {
        return new UpdateItemSectionCommand(id, Name, FormTemplateId );
    }
}