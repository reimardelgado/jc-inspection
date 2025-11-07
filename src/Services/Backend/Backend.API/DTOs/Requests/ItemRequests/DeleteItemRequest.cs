
using Backend.Application.Commands.ItemCommands;

namespace Backend.API.DTOs.Requests.ItemRequests;

public class DeleteItemRequest
{
    public DeleteItemCommand ToApplicationRequest(Guid id)
    {
        return new DeleteItemCommand(id);
    }
}