using Backend.Application.DTOs.Responses.FormTemplateResponses;
using Backend.Application.Queries.ItemSectionQueries;
using Backend.Application.Specifications.FormTemplateSpecs;

namespace Backend.Application.Queries.FormTemplateQueries
{
    public class ReadAllFormTemplatesQueryHandler : IRequestHandler<ReadAllFormTemplatesQuery,
        EntityResponse<List<FormTemplateModelResponse>>>
    {
        private readonly IRepository<FormTemplate> _repository;
        private readonly IMediator _mediator;

        public ReadAllFormTemplatesQueryHandler(IRepository<FormTemplate> repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }

        public async Task<EntityResponse<List<FormTemplateModelResponse>>> Handle(ReadAllFormTemplatesQuery query,
            CancellationToken cancellationToken)
        {
            var spec = new FormTemplateSpec();
            var response = new List<FormTemplateModelResponse>();
            //Get entity list
            var entityCollection = await _repository.ListAsync(spec, cancellationToken);
            foreach (var formTemplate in entityCollection)
            {
                var querySection = new ReadAllItemSectionsQuery(formTemplate.Id);
                var queryResult = await _mediator.Send(querySection, cancellationToken);
                var temp = new FormTemplateModelResponse()
                {
                    Id = formTemplate.Id,
                    Name = formTemplate.Name,
                    Description = formTemplate.Description,
                    Sections = queryResult.Value
                };
                response.Add(temp);
            }

            return response;
        }
    }
}