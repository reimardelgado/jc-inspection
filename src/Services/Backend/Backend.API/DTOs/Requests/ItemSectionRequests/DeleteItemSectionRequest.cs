
using Backend.Application.Commands.ItemSectionCommands;

namespace Backend.API.DTOs.Requests.ItemSectionRequests;

public class DeleteItemSectionRequest
{
    public DeleteItemSectionCommand ToApplicationRequest(Guid id)
    {
        return new DeleteItemSectionCommand(id);
    }
}