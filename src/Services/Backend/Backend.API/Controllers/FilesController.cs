using Amazon.Runtime.Internal;
using Backend.API.DTOs.Requests.FileRequests;
using Backend.Application.DTOs.Responses.Photos;

namespace Backend.API.Controllers
{
    [Route("api/v1/files")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        #region Constructor & properties

        private readonly ILogger<FilesController> _logger;
        private readonly IMediator _mediator;
        private readonly IWebHostEnvironment _env;

        public FilesController(ILogger<FilesController> logger, IMediator mediator, IWebHostEnvironment env)
        {
            _logger = logger;
            _mediator = mediator;
            _env = env;
        }

        #endregion
        
        [HttpGet]
        // [JwtAuthorize(JwtScope.Manager)]
        [AllowAnonymous]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [Produces(typeof(IReadOnlyCollection<PhotoResponse>))]
        [Route("inspection/{inspectionId}/section/{sectionId}/all")]
        public async Task<IActionResult> ReadAllFiles(string inspectionId, string sectionId)
        {
            var request = new ReadAllPhotosRequest(inspectionId, sectionId);
            var contentRootPath = _env.ContentRootPath;
            var query = request.ToApplicationRequest(contentRootPath);
            var response = await _mediator.Send(query);
            
            return Ok(response);
        }

        #region Http Post Methods

        [HttpPost]
        // [JwtAuthorize(JwtScope.Manager)]
        [AllowAnonymous]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [Produces(typeof(bool))]
        [ProducesErrorResponseType(typeof(ErrorResponse))]
        public async Task<IActionResult> CreateFile([FromBody] CreateFileRequest request)
        {
            var contentRootPath = _env.ContentRootPath;
            var command = request.ToApplicationRequest(contentRootPath);
            await _mediator.Send(command);
            // }

            return Ok();
        }

        #endregion

        //

        #region Http Delete Methods

        [HttpDelete]
        // [JwtAuthorize(JwtScope.Manager)]
        [AllowAnonymous]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesErrorResponseType(typeof(ErrorResponse))]
        public async Task<IActionResult> DeleteFile([FromBody] DeleteFileRequest request)
        {
            var contentRootPath = _env.ContentRootPath;
            var command = request.ToApplicationRequest(contentRootPath);

            _logger.LogInformation(
                "----- Sending command: {CommandName} {@Command})",
                nameof(command),
                command);

            var response = await _mediator.Send(command);
            if (!response.IsSuccess)
            {
                return BadRequest();
            }

            return Ok();
        }

        #endregion
    }
}