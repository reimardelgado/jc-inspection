using Backend.API.DTOs.Requests.NotificationRequests;
using Backend.API.DTOs.Requests.UserRequests;
using ServiceReference;

namespace Backend.API.Controllers;

public class NotificationsController : BaseController
{
    #region Contructor && Properties

    public NotificationsController(IMediator mediator) : base(mediator)
    {
    }

    #endregion   
    
    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [Route("registertokenmobile")]
    public async Task<IActionResult> RegisterTokenMobile([FromBody] MobileIdRequest request)
    {
        
        var command = request.ToApplicationRequest();
        var response = await Mediator.Send(command);

        if (!response.IsSuccess)
        {
            return BadRequest(response);
        }

        return Ok();
    }
    
    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [Route("sendemail")]
    public async Task<IActionResult> SendEmail([FromBody] EmailNotificationRequest request)
    {
        var command = request.ToApplicationRequest();
        var response = await Mediator.Send(command);

        if (!response.IsSuccess)
        {
            return BadRequest(response);
        }

        return Ok();
    }
}