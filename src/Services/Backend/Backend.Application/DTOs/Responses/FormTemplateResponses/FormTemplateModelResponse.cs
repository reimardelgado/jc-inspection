using Backend.Application.DTOs.Responses.ItemSectionResponses;

namespace Backend.Application.DTOs.Responses.FormTemplateResponses;

public class FormTemplateModelResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<ItemSectionModelResponse> Sections { get; set; }

    
}

