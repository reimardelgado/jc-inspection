using Backend.Application.Commands.CatalogCommands;
using Backend.Domain.SeedWork;

namespace Backend.API.DTOs.Requests.CatalogRequests;

public class CreateCatalogRequest
{
    public string Name { get; set; }
    public List<string> CatalogValueNames { get; set; }

    public CreateCatalogRequest(string name, List<string> catalogValueNames)
    {
        Name = name;
        CatalogValueNames = catalogValueNames;
    }

    public CreateCatalogCommand ToApplicationRequest()
    {
        return new CreateCatalogCommand(Name, StatusEnum.Active, CatalogValueNames);
    }
}