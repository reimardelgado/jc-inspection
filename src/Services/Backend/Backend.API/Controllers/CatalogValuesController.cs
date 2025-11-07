using Backend.API.DTOs.Requests.CatalogValueRequests;
using Backend.Application.DTOs.Responses.CatalogValueResponses;
using Backend.Application.Queries.CatalogValueQueries;

namespace Backend.API.Controllers;

public class CatalogValuesController : BaseController
{
    #region Contructor & Properties

    public CatalogValuesController(IMediator mediator) : base(mediator)
    {
    }

    #endregion

    [HttpGet]
    // [JwtAuthorize(JwtScope.Manager)]    
    [AllowAnonymous]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [Produces(typeof(IReadOnlyCollection<CatalogValueResponse>))]
    [Route("")]
    public async Task<IActionResult> ReadCatalogValues([FromQuery] ReadCatalogValuesRequest request)
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
    [Produces(typeof(IReadOnlyCollection<CatalogValueResponse>))]
    [Route("{catalogValueId:guid}/all")]
    public async Task<IActionResult> ReadAllCatalogValues()
    {
        var query = new ReadAllCatalogValuesQuery();

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
    [Produces(typeof(CatalogValueResponse))]
    [Route("{catalogValueId:guid}")]
    public async Task<IActionResult> ReadCatalogValue(Guid catalogValueId)
    {
        var request = new ReadCatalogValueRequest(catalogValueId);
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
    public async Task<IActionResult> CreateCatalogValue([FromBody] CreateCatalogValueRequest request)
    {
        var command = request.ToApplicationRequest();

        var response = await Mediator.Send(command);
        if (!response.IsSuccess)
        {
            return BadRequest();
        }

        return CreatedAtAction(nameof(ReadCatalogValue), new { Catalog = response.Value }, response.Value);
    }

    [HttpPut]
    // [JwtAuthorize(JwtScope.Manager)]
    [AllowAnonymous]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [Route("{catalogValueId:guid}")]
    public async Task<IActionResult> UpdateCatalogValue(Guid catalogValueId,
        [FromBody] UpdateCatalogValueRequest request)
    {
        var command = request.ToApplicationRequest(catalogValueId);

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
    [Route("{catalogValueId:guid}")]
    public async Task<IActionResult> DeleteCatalogValue(Guid catalogValueId)
    {
        var request = new DeleteCatalogValueRequest();
        var command = request.ToApplicationRequest(catalogValueId);
        var response = await Mediator.Send(command);

        if (!response.IsSuccess)
        {
            return BadRequest(response);
        }

        return Ok(response.Value);
    }
    #endregion
}