using Backend.API.DTOs.Requests.InspectionRequests;
using Backend.API.DTOs.Requests.InspectionResultRequests;
using Backend.Application.DTOs.Responses.InspectionResultResponses;
using Backend.Application.Queries.InspectionQueries;
using Backend.Application.Queries.InspectionResultQueries;
using UpdateInspectionRequest = Backend.API.DTOs.Requests.InspectionRequests.UpdateInspectionRequest;

namespace Backend.API.Controllers;

public class InspectionResultsController : BaseController
{
    #region Contructor & Properties

    public InspectionResultsController(IMediator mediator) : base(mediator)
    {
    }

    #endregion

    [HttpGet]
    // [JwtAuthorize(JwtScope.Manager)]    
    [AllowAnonymous]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [Produces(typeof(IReadOnlyCollection<InspectionResultResponse>))]
    [Route("")]
    public async Task<IActionResult> ReadInspectionResults([FromQuery] ReadInspectionResultsRequest request)
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
    [Produces(typeof(IReadOnlyCollection<InspectionResultResponse>))]
    [Route("all")]
    public async Task<IActionResult> ReadAllInspectionResults()
    {
        var query = new ReadAllInspectionResultsQuery();

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
    [Produces(typeof(InspectionResultResponse))]
    [Route("{inspectionResultId}")]
    public async Task<IActionResult> ReadInspectionResult(string inspectionResultId)
    {
        var request = new ReadInspectionResultRequest(inspectionResultId);
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
    [Produces(typeof(InspectionResultResponse))]
    [Route("inspection/{inspectionId}/template/{templateId}")]
    public async Task<IActionResult> ReadInspectionResult(string inspectionId, string templateId)
    {
        var request = new ReadInspectionResultByInspectionRequest(inspectionId, templateId);
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
    public async Task<IActionResult> CreateInspectionResult([FromBody] CreateInspectionResultRequest request)
    {
        var command = request.ToApplicationRequest();

        var response = await Mediator.Send(command);
        if (!response.IsSuccess)
        {
            return BadRequest();
        }

        return CreatedAtAction(nameof(ReadInspectionResult), new { Inspection = response.Value }, response.Value);
    }

    [HttpPut]
    // [JwtAuthorize(JwtScope.Manager)]
    [AllowAnonymous]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [Route("{inspectionResultId}")]
    public async Task<IActionResult> UpdateInspectionResult(string inspectionResultId,
        [FromBody] UpdateInspectionResultRequest request)
    {
        var command = request.ToApplicationRequest(inspectionResultId);

        var response = await Mediator.Send(command);
        if (!response.IsSuccess)
        {
            return BadRequest();
        }

        return Ok();
    }

    #region HttDelete Methods

    [HttpDelete]
    [JwtAuthorize(JwtScope.Manager)]    
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [Route("{inspectionResultId}")]
    public async Task<IActionResult> DeleteInspectionResult(string inspectionResultId)
    {
        var request = new DeleteInspectionRequest();
        var command = request.ToApplicationRequest(inspectionResultId);
        var response = await Mediator.Send(command);

        if (!response.IsSuccess)
        {
            return BadRequest(response);
        }

        return Ok(response.Value);
    }
    #endregion
}