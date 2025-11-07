
using Backend.Application.Commands.CatalogValueCommands;

namespace Backend.API.DTOs.Requests.CatalogValueRequests;

public class DeleteCatalogValueRequest
{
    public DeleteCatalogValueCommand ToApplicationRequest(Guid id)
    {
        return new DeleteCatalogValueCommand(id);
    }
}