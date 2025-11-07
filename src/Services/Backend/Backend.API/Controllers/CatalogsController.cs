using Backend.API.DTOs.Requests.CatalogRequests;
using Backend.Application.DTOs.Responses.CatalogResponses;
using Backend.Application.Queries.CatalogQueries;

namespace Backend.API.Controllers;

public class CatalogsController : BaseController
{
    #region Contructor & Properties

    public CatalogsController(IMediator mediator) : base(mediator)
    {
    }

    #endregion

    [HttpGet]
    // [JwtAuthorize(JwtScope.Manager)]    
    [AllowAnonymous]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [Produces(typeof(IReadOnlyCollection<CatalogResponse>))]
    [Route("")]
    public async Task<IActionResult> ReadCatalogs([FromQuery] ReadCatalogsRequest request)
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
    [Produces(typeof(IReadOnlyCollection<CatalogResponse>))]
    [Route("all")]
    public async Task<IActionResult> ReadAllCatalogs()
    {
        var query = new ReadAllCatalogsQuery();

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
    [Produces(typeof(CatalogResponse))]
    [Route("{catalogId:guid}")]
    public async Task<IActionResult> ReadCatalog(Guid catalogId)
    {
        var request = new ReadCatalogRequest(catalogId);
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
    public async Task<IActionResult> CreateCatalog([FromBody] CreateCatalogRequest request)
    {
        var command = request.ToApplicationRequest();

        var response = await Mediator.Send(command);
        if (!response.IsSuccess)
        {
            return BadRequest();
        }

        return CreatedAtAction(nameof(ReadCatalog), new { Catalog = response.Value }, response.Value);
    }

    [HttpPut]
    // [JwtAuthorize(JwtScope.Manager)]
    [AllowAnonymous]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [Route("{catalogId:guid}")]
    public async Task<IActionResult> UpdateCatalog(Guid catalogId,
        [FromBody] UpdateCatalogRequest request)
    {
        var command = request.ToApplicationRequest(catalogId);

        var response = await Mediator.Send(command);
        if (!response.IsSuccess)
        {
            return BadRequest();
        }

        return Ok();
    }

    #region HttDelete Methods

    [HttpDelete]
    // [JwtAuthorize(JwtScope.Manager)]    
    [AllowAnonymous]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [Route("{catalogId:guid}")]
    public async Task<IActionResult> DeleteCatalog(Guid catalogId)
    {
        var request = new DeleteCatalogRequest();
        var command = request.ToApplicationRequest(catalogId);
        var response = await Mediator.Send(command);

        if (!response.IsSuccess)
        {
            return BadRequest(response);
        }

        return Ok(response.Value);
    }
    #endregion
}