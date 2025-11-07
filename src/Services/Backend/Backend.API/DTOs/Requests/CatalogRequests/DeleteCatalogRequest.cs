
using Backend.Application.Commands.CatalogCommands;

namespace Backend.API.DTOs.Requests.CatalogRequests;

public class DeleteCatalogRequest
{
    public DeleteCatalogCommand ToApplicationRequest(Guid id)
    {
        return new DeleteCatalogCommand(id);
    }
}