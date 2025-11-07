using Backend.API.DTOs.Requests.InspectionRequests;
using Backend.Application.Commands.InspectionCommands;
using Backend.Application.DTOs.Responses.InspectionResponses;
using Backend.Application.Queries.InspectionQueries;

namespace Backend.API.Controllers;

public class InspectionsController : BaseController
{
    #region Contructor & Properties
    private readonly IWebHostEnvironment _env;
    public InspectionsController(IMediator mediator, IWebHostEnvironment env) : base(mediator)
    {
        _env = env;
    }

    #endregion

    [HttpGet]
    [JwtAuthorize(JwtScope.Manager)]    
    // [AllowAnonymous]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [Produces(typeof(IReadOnlyCollection<InspectionResponse>))]
    [Route("")]
    public async Task<IActionResult> ReadInspections([FromQuery] ReadInspectionsRequest request)
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
    [JwtAuthorize(JwtScope.Manager)] 
    // [AllowAnonymous]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [Produces(typeof(IReadOnlyCollection<InspectionResponse>))]
    [Route("all")]
    public async Task<IActionResult> ReadAllInspections()
    {
        var claim = HttpContext.User.Claims.FirstOrDefault(t => t.Type.Equals("id"));
        var claimValue = claim?.Value;
        if (claimValue == null)
        {
            return BadRequest();
        }
        var userId = Guid.Parse(claimValue);
        var query = new ReadAllInspectionsQuery(userId);

        var response = await Mediator.Send(query);
        if (!response.IsSuccess)
        {
            return BadRequest();
        }

        return Ok(response.Value);
    }

    [HttpGet]
    [JwtAuthorize(JwtScope.Manager)] 
    // [AllowAnonymous]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [Produces(typeof(InspectionResponse))]
    [Route("{inspectionId:guid}")]
    public async Task<IActionResult> ReadInspection(Guid inspectionId)
    {
        var request = new ReadInspectionRequest(inspectionId);
        var query = request.ToApplicationRequest();

        var response = await Mediator.Send(query);
        if (!response.IsSuccess)
        {
            return BadRequest();
        }

        return Ok(response.Value);
    }

    [HttpPost]
    [JwtAuthorize(JwtScope.Manager)] 
    // [AllowAnonymous]
    [ProducesResponseType((int)HttpStatusCode.Created)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> CreateInspection([FromBody] CreateInspectionRequest request)
    {
        var command = request.ToApplicationRequest();

        var response = await Mediator.Send(command);
        if (!response.IsSuccess)
        {
            return BadRequest();
        }

        return CreatedAtAction(nameof(ReadInspection), new { Inspection = response.Value }, response.Value);
    }

    [HttpPut]
    [JwtAuthorize(JwtScope.Manager)]
    // [AllowAnonymous]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [Route("{inspectionId:guid}")]
    public async Task<IActionResult> UpdateInspection(string inspectionId,
        [FromBody] UpdateInspectionRequest request)
    {
        var command = request.ToApplicationRequest(inspectionId);

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
    [Route("{inspectionId:guid}")]
    public async Task<IActionResult> DeleteInspection(string inspectionId)
    {
        var request = new DeleteInspectionRequest();
        var command = request.ToApplicationRequest(inspectionId);
        var response = await Mediator.Send(command);

        if (!response.IsSuccess)
        {
            return BadRequest(response);
        }

        return Ok(response.Value);
    }
    #endregion
    
    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [Route("{inspectionId:guid}/report")]
    public async Task<IActionResult> DownloadReportInspection(Guid inspectionId)
    {
        var contentRootPath = _env.ContentRootPath;
        var command = new CreateInspectionReportCommand(inspectionId, contentRootPath);
        var response = await Mediator.Send(command);

        if (!response.IsSuccess)
        {
            return BadRequest(response);
        }
        var pdfContentType = "application/pdf";
        return File(response.Value!, pdfContentType,$"Reporte-{DateTime.Now.Ticks}.pdf");
        //var base64 = Convert.ToBase64String(response.Value);
        //return Ok(base64);
    }
    
    [HttpGet]
    [JwtAuthorize(JwtScope.Manager)] 
    // [AllowAnonymous]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [Produces(typeof(InspectionResponse))]
    [Route("{inspectionId:guid}/zoho")]
    public async Task<IActionResult> ReadInspectionZoho(Guid inspectionId)
    {
        var request = new ReadInspectionZohoRequest(inspectionId);
        var query = request.ToApplicationRequest();

        var response = await Mediator.Send(query);
        if (!response.IsSuccess)
        {
            return BadRequest();
        }

        return Ok(response.Value);
    }
    
    [HttpGet]
    [JwtAuthorize(JwtScope.Manager)] 
    // [AllowAnonymous]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [Produces(typeof(bool))]
    [Route("{inspectionId}/sendToZoho")]
    public async Task<IActionResult> SendInspectionZoho(string inspectionId)
    {
        var scheme = HttpContext.Request.IsHttps;
        var request = new SendInspectionZohoRequest(inspectionId);
        var query = request.ToApplicationRequest();

        var response = await Mediator.Send(query);
        if (!response.IsSuccess)
        {
            return BadRequest();
        }

        return Ok(response.Value);
    }
    
    [HttpGet]
    [JwtAuthorize(JwtScope.Manager)]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [Route("{inspectionId}/approve")]
    public async Task<IActionResult> UpdateStatusApproveInspection(string inspectionId)
    {
        var claim = HttpContext.User.Claims.FirstOrDefault(t => t.Type.Equals("id"));
        var claimValue = claim?.Value;
        if (claimValue == null)
        {
            return BadRequest();
        }

        var managerUserId = Guid.Parse(claimValue);
        var command = new UpdateInspectionApproveStatusCommand(Guid.Parse(inspectionId), managerUserId);

        var response = await Mediator.Send(command);
        if (!response.IsSuccess)
        {
            return BadRequest();
        }

        return Ok();
    }

    [HttpPost]
    [JwtAuthorize(JwtScope.Manager)]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [Route("{inspectionId}/reject")]
    public async Task<IActionResult> UpdateStatusRejectOrder([FromBody]UpdateInspectionRejectStatusRequest request, Guid inspectionId)
    {
        var claim = HttpContext.User.Claims.FirstOrDefault(t => t.Type.Equals("id"));
        var claimValue = claim?.Value;
        if (claimValue == null)
        {
            return BadRequest();
        }

        var managerUserId = Guid.Parse(claimValue);
        var command = request.ToApplicationRequest(inspectionId, managerUserId);

        var response = await Mediator.Send(command);
        if (!response.IsSuccess)
        {
            return BadRequest();
        }

        return Ok();
    }
}