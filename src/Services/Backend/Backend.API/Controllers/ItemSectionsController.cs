using Backend.API.DTOs.Requests.ItemSectionRequests;
using Backend.Application.DTOs.Responses.ItemSectionResponses;
using Backend.Application.Queries.ItemSectionQueries;

namespace Backend.API.Controllers;

public class ItemSectionsController : BaseController
{
    #region Contructor & Properties

    public ItemSectionsController(IMediator mediator) : base(mediator)
    {
    }

    #endregion

    [HttpGet]
    // [JwtAuthorize(JwtScope.Manager)]    
    [AllowAnonymous]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [Produces(typeof(IReadOnlyCollection<ItemSectionResponse>))]
    [Route("")]
    public async Task<IActionResult> ReadItemSections([FromQuery] ReadItemSectionsRequest request)
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
    [Produces(typeof(IReadOnlyCollection<ItemSectionResponse>))]
    [Route("all")]
    public async Task<IActionResult> ReadAllItemSections([FromQuery] ReadAllItemSectionsRequest request)
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
    [Produces(typeof(ItemSectionResponse))]
    [Route("{itemSectionId:guid}")]
    public async Task<IActionResult> ReadItemSection(Guid itemSectionId)
    {
        var request = new ReadItemSectionRequest(itemSectionId);
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
    public async Task<IActionResult> CreateItemSection([FromBody] CreateItemSectionRequest request)
    {
        var command = request.ToApplicationRequest();

        var response = await Mediator.Send(command);
        if (!response.IsSuccess)
        {
            return BadRequest();
        }

        return CreatedAtAction(nameof(ReadItemSection), new { ItemSection = response.Value }, response.Value);
    }

    [HttpPut]
    // [JwtAuthorize(JwtScope.Manager)]
    [AllowAnonymous]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [Route("{itemSectionId:guid}")]
    public async Task<IActionResult> UpdateItemSection(Guid itemSectionId,
        [FromBody] UpdateItemSectionRequest request)
    {
        var command = request.ToApplicationRequest(itemSectionId);

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
    [Route("{itemSectionId:guid}")]
    public async Task<IActionResult> DeleteItemSection(Guid itemSectionId)
    {
        var request = new DeleteItemSectionRequest();
        var command = request.ToApplicationRequest(itemSectionId);
        var response = await Mediator.Send(command);

        if (!response.IsSuccess)
        {
            return BadRequest(response);
        }

        return Ok(response.Value);
    }
    #endregion
}