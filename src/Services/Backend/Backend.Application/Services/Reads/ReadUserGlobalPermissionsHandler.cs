using Backend.Application.Specifications.UserGlobalPermissionSpecs;

namespace Backend.Application.Services.Reads
{
    public class
        ReadUserGlobalPermissionsHandler : IRequestHandler<ReadUserGlobalPermissions, ICollection<UserGlobalPermission>>
    {
        private readonly IReadRepository<UserGlobalPermission> _userGlobalsPermissionRepository;

        public ReadUserGlobalPermissionsHandler(IReadRepository<UserGlobalPermission> userGlobalsPermissionRepository)
        {
            _userGlobalsPermissionRepository = userGlobalsPermissionRepository;
        }

        public async Task<ICollection<UserGlobalPermission>> Handle(ReadUserGlobalPermissions request,
            CancellationToken cancellationToken)
        {
            var permissions = await _userGlobalsPermissionRepository.ListAsync(
                new UserGlobalPermissionByUserIdSpec(request.UserId), cancellationToken);

            return permissions;
        }
    }
}