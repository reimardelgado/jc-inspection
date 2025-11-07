using Backend.Application.Commands.AuthJwtCommands;
using Backend.Application.DTOs.Responses.UserResponses;
using Backend.Application.Queries.UserQueries;
using Backend.Application.Services.Reads;
using Backend.Application.Utils;

namespace Backend.Application.Commands.UserCommands
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, EntityResponse<ReadUserMeResponse>>
    {
        private readonly IMediator _mediator;
        private readonly IRepository<User> _repository;

        private User? _user;

        public LoginUserCommandHandler(IMediator mediator, IRepository<User> repository)
        {
            _mediator = mediator;
            _repository = repository;
        }

        public async Task<EntityResponse<ReadUserMeResponse>> Handle(LoginUserCommand command, CancellationToken cancellationToken)
        {
            var user = await _mediator.Send(new ReadUserService(command.Username, command.Password), cancellationToken);
            if (user is null)
            {
                return EntityResponse<ReadUserMeResponse>.Error(EntityResponseUtils.GenerateMsg(MessageHandler.AuthenticationError));
            }

            //Generate JWT
            var createJwtResponse = await _mediator.Send(new CreateJwtCommand(user!.Id), cancellationToken);
            if (!createJwtResponse.IsSuccess)
                return EntityResponse<ReadUserMeResponse>.Error(EntityResponseUtils.GenerateMsg(MessageHandler.AuthenticationError));

            var userMe = await _mediator.Send(new ReadUserMeQuery(user!.Id, createJwtResponse.Value.Jwt), cancellationToken);
            if (!userMe.IsSuccess)
            {
                return EntityResponse<ReadUserMeResponse>.Error(EntityResponseUtils.GenerateMsg(MessageHandler.AuthenticationError));
            }
            
            return userMe;
        }

        #region Private methods

        private async Task<ICollection<string>> GetPermissions(User user, CancellationToken cancellationToken)
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

        private async Task<ICollection<string>> GetScopedPermissions(User user, CancellationToken cancellationToken)
        {
            var profilesIds = user.UserProfiles.Select(profile => profile.ProfileId).ToList();

            var profilesPermissions = await _mediator.Send(new ReadProfilePermissionService(profilesIds),
                cancellationToken);

            var permissions = profilesPermissions.Select(permission => permission.Permission);
            var permissionCodes = permissions
                .Select(permission => permission.Code)
                .Distinct()
                .ToList();

            return permissionCodes;
        }

        #endregion
    }
}