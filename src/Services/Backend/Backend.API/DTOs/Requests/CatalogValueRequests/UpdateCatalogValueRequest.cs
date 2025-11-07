using Backend.Application.Commands.CatalogValueCommands;

namespace Backend.API.DTOs.Requests.CatalogValueRequests;

public class UpdateCatalogValueRequest
{
    public string Name { get; set; }
    
    public UpdateCatalogValueRequest(string name)
    {
        Name = name;
    }

    public UpdateCatalogValueCommand ToApplicationRequest(Guid id)
    {
        return new UpdateCatalogValueCommand(id, Name );
    }
}