using Backend.Application.Commands.CatalogValueCommands;
using Backend.Domain.SeedWork;

namespace Backend.API.DTOs.Requests.CatalogValueRequests;

public class CreateCatalogValueRequest
{
    public string Name { get; set; }
    public Guid CatalogId { get; set; }

    public CreateCatalogValueRequest(string name, Guid catalogId)
    {
        Name = name;
        CatalogId = catalogId;
    }

    public CreateCatalogValueCommand ToApplicationRequest()
    {
        return new CreateCatalogValueCommand(Name, StatusEnum.Active, CatalogId);
    }
}