using Backend.API.DTOs.Requests.ProfileRequests;
using Backend.Application.DTOs.Responses.ProfileResponses;
using Backend.Application.Queries.ProfileQueries;

namespace Backend.API.Controllers;

public class ProfileController : BaseController
{
    #region Contructor && Properties

    public ProfileController(IMediator mediator) : base(mediator)
    {
    }

    #endregion

    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType((int) HttpStatusCode.OK)]
    [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
    public async Task<IActionResult> ReadProfiles([FromQuery] ReadProfilesRequest request)
    {
        var query = request.ToApplicationRequest();
        var response = await Mediator.Send(query);

        return Ok(response);
    }

    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [Produces(typeof(IReadOnlyCollection<ReadProfilesResponse>))]
    [Route("all")]
    public async Task<IActionResult> ReadAllProfiles()
    {
        var query = new ReadAllProfilesQuery();

        var response = await Mediator.Send(query);
        if (!response.IsSuccess)
        {
            return BadRequest();
        }

        return Ok(response);
    }

    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType((int) HttpStatusCode.OK)]
    [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
    [Produces(typeof(List<PermissionResponse>))]
    [Route("permission")]
    public async Task<IActionResult> ReadPermission()
    {
        var query = new ReadPermissionQuery();
        var response = await Mediator.Send(query);

        return Ok(response);
    }

    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType((int) HttpStatusCode.OK)]
    [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
    [Route("{profileId:guid}")]
    public async Task<IActionResult> ReadProfiles(Guid profileId)
    {
        var query = new ReadProfileQuery(profileId);
        var response = await Mediator.Send(query);
        if (!response.IsSuccess)
        {
            return BadRequest(response);
        }

        return Ok(response.Value);
    }
    
    [HttpPost]
    [ProducesResponseType((int) HttpStatusCode.OK)]
    [ProducesResponseType((int) HttpStatusCode.BadRequest)]
    public async Task<IActionResult> CreateProfile([FromBody] CreateProfileRequest request)
    {
        var query = request.ToApplicationRequest();

        var response = await Mediator.Send(query);
        if (!response.IsSuccess)
        {
            return BadRequest(response);
        }

        return Ok(response.Value);
    }
    
    [HttpPatch]
    [ProducesResponseType((int) HttpStatusCode.OK)]
    [ProducesResponseType((int) HttpStatusCode.BadRequest)]
    [Route("{profileId:guid}")]
    public async Task<IActionResult> UpdateProfile(Guid profileId, [FromBody] UpdateProfileRequest request)
    {
        var query = request.ToApplicationRequest(profileId);

        var response = await Mediator.Send(query);
        if (!response.IsSuccess)
        {
            return BadRequest(response);
        }

        return Ok(response.Value);
    }
    
    #region HttDelete Methods
    
    [HttpDelete]
    [ProducesResponseType((int) HttpStatusCode.OK)]
    [ProducesResponseType((int) HttpStatusCode.BadRequest)]
    [Route("{profileId:guid}")]
    public async Task<IActionResult> DeleteProfile(Guid profileId)
    {
        var request = new DeleteProfileRequest();
        var command = request.ToApplicationRequest(profileId);
        var response = await Mediator.Send(command);

        if (!response.IsSuccess)
        {
            return BadRequest(response);
        }

        return Ok(response.Value);
    }
    #endregion
}