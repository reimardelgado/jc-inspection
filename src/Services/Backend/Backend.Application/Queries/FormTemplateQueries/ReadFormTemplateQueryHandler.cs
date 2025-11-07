using Backend.Application.DTOs.Responses.FormTemplateResponses;
using Backend.Application.Queries.ItemSectionQueries;

namespace Backend.Application.Queries.FormTemplateQueries
{
    public class ReadFormTemplateQueryHandler : IRequestHandler<ReadFormTemplateQuery,
        EntityResponse<FormTemplateModelResponse>>
    {
        private readonly IRepository<FormTemplate> _repository;
        private readonly IMediator _mediator;
        private FormTemplate? _entity;

        public ReadFormTemplateQueryHandler(IRepository<FormTemplate> repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }


        public async Task<EntityResponse<FormTemplateModelResponse>> Handle(ReadFormTemplateQuery query,
            CancellationToken cancellationToken)
        {
            var validateResponse = await Validate(query, cancellationToken);
            if (!validateResponse.IsSuccess)
            {
                return EntityResponse<FormTemplateModelResponse>.Error(validateResponse);
            }

            var entity = await _repository.GetByIdAsync(query.FormTemplateId, cancellationToken);
            
            var querySection = new ReadAllItemSectionsQuery(entity.Id);
            var queryResult = await _mediator.Send(querySection, cancellationToken);
            var response = new FormTemplateModelResponse()
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                Sections = queryResult.Value
            };
            return response;
        }

        private async Task<EntityResponse<bool>> Validate(ReadFormTemplateQuery query,
            CancellationToken cancellationToken)
        {
            _entity = await _repository.GetByIdAsync(query.FormTemplateId, cancellationToken);
            return _entity is null
                ? EntityResponse<bool>.Error(MessageHandler.FormTemplateNotFound)
                : EntityResponse.Success(true);
        }
    }
}