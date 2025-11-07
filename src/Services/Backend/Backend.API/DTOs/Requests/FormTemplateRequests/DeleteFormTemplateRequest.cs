
using Backend.Application.Commands.FormTemplateCommands;

namespace Backend.API.DTOs.Requests.FormTemplateRequests;

public class DeleteFormTemplateRequest
{
    public DeleteFormTemplateCommand ToApplicationRequest(Guid id)
    {
        return new DeleteFormTemplateCommand(id);
    }
}