using Backend.Application.Services.Reads;
using Shared.Domain.DTOs.JWT;
using Shared.Domain.Interfaces.Services;

namespace Backend.Application.Commands.AuthJwtCommands
{
    public class CreateJwtCommandHandler : IRequestHandler<CreateJwtCommand, EntityResponse<JwtResponse>>
    {
        private readonly ILogger<CreateJwtCommandHandler> _logger;
        private readonly IMediator _mediator;
        private readonly IJwtTokenService _jwtTokenService;

        private User? _user;

        public CreateJwtCommandHandler(ILogger<CreateJwtCommandHandler> logger, IMediator mediator,
            IJwtTokenService jwtTokenService)
        {
            _logger = logger;
            _mediator = mediator;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<EntityResponse<JwtResponse>> Handle(CreateJwtCommand command,
            CancellationToken cancellationToken)
        {
            var validateResponse = await Validate(command, cancellationToken);
            if (!validateResponse.IsSuccess)
            {
                return EntityResponse<JwtResponse>.Error(validateResponse);
            }

            _logger.LogWarning("Generate the accessToken for user: {@UserId}", _user!.Id);

            _logger.LogWarning("JWT Payload");
            var payload = await CreateJwtPayload(cancellationToken);
            var jwtToken = _jwtTokenService.GenerateJwtToken(payload);

            return EntityResponse.Success(new JwtResponse(jwtToken));
        }

        #region Private methods

        private async Task<EntityResponse<bool>> Validate(CreateJwtCommand command, CancellationToken cancellationToken)
        {
            var userResponse = await _mediator.Send(new ReadUserService(command.UserId), cancellationToken);
            if (userResponse is null)
            {
                return EntityResponse<bool>.Error(MessageHandler.GenericError);
            }

            _user = userResponse;

            return true;
        }

        private async Task<JwtPayloadDto> CreateJwtPayload(CancellationToken cancellationToken)
        {
            JwtPayloadDto payload;

            // Manager user
            _logger.LogInformation("Creating jwt for manager user {@ManagerUser}", _user);

            var permissions = await GetPermissions(_user, cancellationToken);
            payload = new JwtPayloadDto(_user!.Id, _user.FullName, UserTypes.Manager, permissions);

            return payload;
        }

        private async Task<ICollection<string>> GetPermissions(User user,
            CancellationToken cancellationToken)
        {
            var scopedPermissions = await GetScopedPermissions(user, cancellationToken);
            var globalPermissions = await GetGlobalPermissions(user.Id, cancellationToken);

            var permissions = scopedPermissions.Concat(globalPermissions).Distinct().ToList();

            return permissions;
        }

        private async Task<ICollection<string>> GetGlobalPermissions(Guid userId, CancellationToken cancellationToken)
        {
            var globalPermissions = await _mediator.Send(new ReadUserGlobalPermissions(userId), cancellationToken);
            var permissionsIds = globalPermissions.Select(permission => permission.PermissionId).ToList();

            var permissions = await _mediator.Send(new ReadPermissionsService(permissionsIds), cancellationToken);
            var permissionsCodes = permissions.Select(permission => permission.Code);

            return permissionsCodes.ToList();
        }

        private async Task<ICollection<string>> GetScopedPermissions(User user,
            CancellationToken cancellationToken)
        {
            var profilesIds = user.UserProfiles.Select(profile => profile.ProfileId).ToList();

            var profilesPermissions = await _mediator.Send(new ReadProfilePermissionService(profilesIds),
                cancellationToken);

            var permissions = profilesPermissions.Select(permission => permission.Permission);
            var permissionCodes = permissions
                .Select(permission => permission!.Code)
                .Distinct()
                .ToList();

            return permissionCodes;
        }

        #endregion
    }
}