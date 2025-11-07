using Backend.API.DTOs.Requests.ItemRequests;
using Backend.Application.DTOs.Responses.ItemResponses;
using Backend.Application.Queries.ItemQueries;

namespace Backend.API.Controllers;

public class ItemsController : BaseController
{
    #region Contructor & Properties

    public ItemsController(IMediator mediator) : base(mediator)
    {
    }

    #endregion

    [HttpGet]
    // [JwtAuthorize(JwtScope.Manager)]    
    [AllowAnonymous]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [Produces(typeof(IReadOnlyCollection<ItemResponse>))]
    [Route("")]
    public async Task<IActionResult> ReadItems([FromQuery] ReadItemsRequest request)
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
    [Produces(typeof(IReadOnlyCollection<ItemResponse>))]
    [Route("all")]
    public async Task<IActionResult> ReadAllItems([FromQuery] ReadAllItemsRequest request)
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
    [Produces(typeof(ItemResponse))]
    [Route("{itemId:guid}")]
    public async Task<IActionResult> ReadItem(Guid itemId)
    {
        var request = new ReadItemRequest(itemId);
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
    public async Task<IActionResult> CreateItem([FromBody] CreateItemRequest request)
    {
        var command = request.ToApplicationRequest();

        var response = await Mediator.Send(command);
        if (!response.IsSuccess)
        {
            return BadRequest();
        }

        return CreatedAtAction(nameof(ReadItem), new { Item = response.Value }, response.Value);
    }

    [HttpPut]
    // [JwtAuthorize(JwtScope.Manager)]
    [AllowAnonymous]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [Route("{itemId:guid}")]
    public async Task<IActionResult> UpdateItem(Guid itemId,
        [FromBody] UpdateItemRequest request)
    {
        var command = request.ToApplicationRequest(itemId);

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
    [Route("{itemId:guid}")]
    public async Task<IActionResult> DeleteItem(Guid itemId)
    {
        var request = new DeleteItemRequest();
        var command = request.ToApplicationRequest(itemId);
        var response = await Mediator.Send(command);

        if (!response.IsSuccess)
        {
            return BadRequest(response);
        }

        return Ok(response.Value);
    }
    #endregion
}