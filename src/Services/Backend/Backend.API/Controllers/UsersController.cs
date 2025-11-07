using Backend.API.DTOs.Requests.ManagerUserRequests;
using Backend.API.DTOs.Requests.UserRequests;
using Backend.Application.DTOs.Responses.ManagerUserResponses;
using Backend.Application.Queries.ManagerUserQueries;
using Backend.Application.Queries.UserQueries;

namespace Backend.API.Controllers;

public class UsersController : BaseController
{
    #region Contructor && Properties

    public UsersController(IMediator mediator) : base(mediator)
    {
    }

    #endregion
    
    [AllowAnonymous]
    [HttpPost]
    [ProducesResponseType((int) HttpStatusCode.OK)]
    [ProducesResponseType((int) HttpStatusCode.BadRequest)]
    [Produces(typeof(Guid))]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] ValidateLoginRequest request)
    {
        var command = request.ToApplicationRequest();
        var response = await Mediator.Send(command);

        if (!response.IsSuccess)
        {
            return BadRequest(response);
        }

        return Ok(response.Value);
    }

    //[HttpGet]
    //[AllowAnonymous]
    //[ProducesResponseType((int)HttpStatusCode.OK)]
    //[ProducesResponseType((int)HttpStatusCode.NotFound)]
    //[Produces(typeof(ReadUserMeResponse))]
    //[Route("me")]
    //public async Task<IActionResult> Me()
    //{
    //    var claim = HttpContext.User.Claims.FirstOrDefault(t => t.Type.Equals("id"));
    //    var claimValue = claim?.Value;
    //    if (claimValue == null)
    //    {
    //        return BadRequest();
    //    }

    //    var userId = Guid.Parse(claimValue);
    //    var command = new ReadUserMeQuery(userId);

    //    var result = await Mediator.Send(command);

    //    if (!result.IsSuccess)
    //    {
    //        return BadRequest();
    //    }

    //    return Ok(result.Value);
    //}
    
    [HttpGet]
    // [JwtAuthorize(JwtScope.Manager)]
    [AllowAnonymous]
    [ProducesResponseType((int) HttpStatusCode.OK)]
    [ProducesResponseType((int) HttpStatusCode.BadRequest)]
    [Produces(typeof(IReadOnlyCollection<ReadUsersResponse>))]
    public async Task<IActionResult> ReadUsers([FromQuery] ReadUsersRequest request)
    {
        var query = request.ToApplicationRequest();

        var response = await Mediator.Send(query);
        if (!response.IsSuccess)
        {
            return BadRequest(response);
        }

        return Ok(response.Value);
    }

    [HttpGet]
    // [JwtAuthorize(JwtScope.Manager)]
    [AllowAnonymous]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [Produces(typeof(IReadOnlyCollection<ReadUsersResponse>))]
    [Route("all")]
    public async Task<IActionResult> ReadAllUsers()
    {
        var query = new ReadAllManagerUsersQuery();

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
    [ProducesResponseType((int) HttpStatusCode.OK)]
    [ProducesResponseType((int) HttpStatusCode.NotFound)]
    [Produces(typeof(ReadUserResponse))]
    [Route("{managerId:guid}")]
    public async Task<IActionResult> ReadUser(Guid managerId)
    {
        var query = new ReadManagerUserQuery(managerId);

        var response = await Mediator.Send(query);
        if (!response.IsSuccess)
        {
            return BadRequest(response);
        }

        return Ok(response.Value);
    }
    
    [HttpPut]
    // [JwtAuthorize(JwtScope.Manager)]
    [AllowAnonymous]
    [ProducesResponseType((int) HttpStatusCode.OK)]
    [ProducesResponseType((int) HttpStatusCode.BadRequest)]
    [Route("{managerId:guid}")]
    public async Task<IActionResult> UpdateUser(Guid managerId, [FromBody] UpdateUserRequest request)
    {
        var query = request.ToApplicationRequest(managerId);

        var response = await Mediator.Send(query);
        if (!response.IsSuccess)
        {
            return BadRequest(response);
        }

        return Ok();
    }
    
    [HttpPatch]
    // [JwtAuthorize(JwtScope.Manager)]
    [AllowAnonymous]
    [ProducesResponseType((int) HttpStatusCode.OK)]
    [ProducesResponseType((int) HttpStatusCode.BadRequest)]
    [Route("{managerId:guid}")]
    public async Task<IActionResult> UpdateProfileUser(Guid managerId, [FromBody] UpdateProfileManagerUserRequest request)
    {
        var query = request.ToApplicationRequest(managerId);

        var response = await Mediator.Send(query);
        if (!response.IsSuccess)
        {
            return BadRequest(response);
        }

        return Ok();
    }
    
    [HttpPost]
    // [JwtAuthorize(JwtScope.Manager)]
    [AllowAnonymous]
    [ProducesResponseType((int) HttpStatusCode.OK)]
    [ProducesResponseType((int) HttpStatusCode.BadRequest)]
    [Produces(typeof(JwtResponse))]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
    {
        var command = request.ToApplicationRequest();
        var response = await Mediator.Send(command);

        if (!response.IsSuccess)
        {
            return BadRequest(response);
        }

        return CreatedAtAction(nameof(ReadUser), new { User = response.Value }, response.Value);
    }

    [HttpPost]
    // [JwtAuthorize(JwtScope.Manager)]
    [AllowAnonymous]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [Route("{managerId:guid}/changepassword")]
    public async Task<IActionResult> UpdatePasswordUser(Guid managerId, [FromBody] UpdatePasswordUserRequest request)
    {
        var query = request.ToApplicationRequest(managerId);

        var response = await Mediator.Send(query);
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
    [Route("recoverpassword")]
    public async Task<IActionResult> RecoverPasswordUser([FromBody] RecoverPasswordRequest request)
    {
        var query = request.ToApplicationRequest();

        var response = await Mediator.Send(query);
        if (!response.IsSuccess)
        {
            return BadRequest(response);
        }

        return Ok();
    }

    #region HttDelete Methods

    [HttpDelete]
    // [JwtAuthorize(JwtScope.Manager)]
    [AllowAnonymous]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [Route("{userId:guid}")]
    public async Task<IActionResult> DeleteUser(Guid userId)
    {
        var request = new DeleteUserRequest();
        var command = request.ToApplicationRequest(userId);
        var response = await Mediator.Send(command);

        if (!response.IsSuccess)
        {
            return BadRequest(response);
        }

        return Ok(response.Value);
    }
    #endregion
}