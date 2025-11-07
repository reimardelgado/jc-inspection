using Backend.API.DTOs.Requests.FormTemplateRequests;
using Backend.Application.DTOs.Responses.FormTemplateResponses;
using Backend.Application.Queries.FormTemplateQueries;

namespace Backend.API.Controllers;

public class FormTemplatesController : BaseController
{
    #region Contructor & Properties

    public FormTemplatesController(IMediator mediator) : base(mediator)
    {
    }

    #endregion

    [HttpGet]
    // [JwtAuthorize(JwtScope.Manager)]    
    [AllowAnonymous]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [Produces(typeof(IReadOnlyCollection<FormTemplateResponse>))]
    [Route("")]
    public async Task<IActionResult> ReadFormTemplates([FromQuery] ReadFormTemplatesRequest request)
    {
        var query = request.ToApplicationRequest();

        var response = await Mediator.Send(query);
        if (!response.IsSuccess)
        {
            return BadRequest();
        }

        return Ok(response.Value);
    }

    [HttpGet]
    // [JwtAuthorize(JwtScope.Manager)] 
    [AllowAnonymous]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [Produces(typeof(IReadOnlyCollection<FormTemplateResponse>))]
    [Route("all")]
    public async Task<IActionResult> ReadAllFormTemplates()
    {
        var query = new ReadAllFormTemplatesQuery();

        var response = await Mediator.Send(query);
        if (!response.IsSuccess)
        {
            return BadRequest();
        }

        return Ok(response.Value);
    }

    [HttpGet]
    // [JwtAuthorize(JwtScope.Manager)] 
    [AllowAnonymous]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [Produces(typeof(FormTemplateResponse))]
    [Route("{formTemplateId:guid}")]
    public async Task<IActionResult> ReadFormTemplate(Guid formTemplateId)
    {
        var request = new ReadFormTemplateRequest(formTemplateId);
        var query = request.ToApplicationRequest();

        var response = await Mediator.Send(query);
        if (!response.IsSuccess)
        {
            return BadRequest();
        }

        return Ok(response.Value);
    }

    [HttpPost]
    // [JwtAuthorize(JwtScope.Manager)] 
    [AllowAnonymous]
    [ProducesResponseType((int)HttpStatusCode.Created)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> CreateFormTemplate([FromBody] CreateFormTemplateRequest request)
    {
        var command = request.ToApplicationRequest();

        var response = await Mediator.Send(command);
        if (!response.IsSuccess)
        {
            return BadRequest();
        }

        return CreatedAtAction(nameof(ReadFormTemplate), new { FormTemplate = response.Value }, response.Value);
    }

    [HttpPut]
    // [JwtAuthorize(JwtScope.Manager)]
    [AllowAnonymous]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [Route("{formTemplateId:guid}")]
    public async Task<IActionResult> UpdateFormTemplate(Guid formTemplateId,
        [FromBody] UpdateFormTemplateRequest request)
    {
        var command = request.ToApplicationRequest(formTemplateId);

        var response = await Mediator.Send(command);
        if (!response.IsSuccess)
        {
            return BadRequest();
        }

        return CreatedAtAction(nameof(ReadFormTemplate), new { FormTemplate = response.Value }, response.Value);
    }

    #region HttDelete Methods

    [HttpDelete]
    [JwtAuthorize(JwtScope.Manager)]    
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [Route("{formTemplateId:guid}")]
    public async Task<IActionResult> DeleteFormTemplate(Guid formTemplateId)
    {
        var request = new DeleteFormTemplateRequest();
        var command = request.ToApplicationRequest(formTemplateId);
        var response = await Mediator.Send(command);

        if (!response.IsSuccess)
        {
            return BadRequest(response);
        }

        return Ok(response.Value);
    }
    #endregion
}