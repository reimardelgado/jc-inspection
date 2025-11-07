using Backend.Application.Commands.CatalogCommands;

namespace Backend.API.DTOs.Requests.CatalogRequests;

public class UpdateCatalogRequest
{
    public string Name { get; set; }
    
    public UpdateCatalogRequest(string name)
    {
        Name = name;
    }

    public UpdateCatalogCommand ToApplicationRequest(Guid id)
    {
        return new UpdateCatalogCommand(id, Name );
    }
}