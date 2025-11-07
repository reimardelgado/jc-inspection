using Backend.API.DTOs.Requests.CatalogRequests;
using Backend.Application.DTOs.Responses.CatalogResponses;
using Backend.Application.DTOs.Responses.ZohoResponses;
using Backend.Application.Queries.CatalogQueries;
using Backend.Application.Queries.ZohoQueries;

namespace Backend.API.Controllers;

public class ZohoAuthController : BaseController
{
    #region Contructor & Properties

    public ZohoAuthController(IMediator mediator) : base(mediator)
    {
    }

    #endregion

    [HttpGet]
    // [JwtAuthorize(JwtScope.Manager)] 
    [AllowAnonymous]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [Produces(typeof(IReadOnlyCollection<ZohoTokenResponse>))]
    [Route("token")]
    public async Task<IActionResult> ReadZohoAuth()
    {
        var query = new ReadZohoTokenQuery();
        var response = await Mediator.Send(query);
        if (!response.IsSuccess)
        {
            return BadRequest();
        }

        return Ok(response.Value);
    }
}