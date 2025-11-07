using Backend.Application.DTOs.Responses.UserResponses;
using Backend.Application.Services.Reads;

namespace Backend.Application.Queries.UserQueries
{
    public class ReadUserMeQueryHandler : IRequestHandler<ReadUserMeQuery,
        EntityResponse<ReadUserMeResponse>>
    {
        private readonly IMediator _mediator;

        private User? _user;

        public ReadUserMeQueryHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<EntityResponse<ReadUserMeResponse>> Handle(ReadUserMeQuery query,
            CancellationToken cancellationToken)
        {
            var validateResponse = await Validate(query, cancellationToken);
            if (!validateResponse.IsSuccess)
            {
                return EntityResponse<ReadUserMeResponse>.Error(validateResponse);
            }

            var permissions = await GetPermissions(_user!, cancellationToken);

            var response = EntityResponse.Success(new ReadUserMeResponse(_user.Id, _user.Username,
                _user.Email, _user.FullName, _user.Phone, _user.Dni, query.Token,
                permissions));

            return response;
        }

        private async Task<EntityResponse<bool>> Validate(ReadUserMeQuery query,
            CancellationToken cancellationToken)
        {
            // Add the validation logic here
            _user = await _mediator.Send(new ReadUserService(query.UserId), cancellationToken);
            return _user is null
                ? EntityResponse<bool>.Error("User not found")
                : EntityResponse.Success(true);
        }

        #region Private methods

        private async Task<IEnumerable<string>> GetPermissions(User user,
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