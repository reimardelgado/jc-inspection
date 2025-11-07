using Backend.Application.Specifications.ProfilePermissionsSpecs;

namespace Backend.Application.Services.Reads
{
    public class
        ReadProfilePermissionServiceHandler : IRequestHandler<ReadProfilePermissionService, ICollection<ProfilePermission>>
    {
        #region Constructor && Properties

        private readonly IReadRepository<ProfilePermission> _profilePermissionRepository;


        public ReadProfilePermissionServiceHandler(IReadRepository<ProfilePermission> profilePermissionRepository)
        {
            _profilePermissionRepository = profilePermissionRepository;
        }

        #endregion

        public async Task<ICollection<ProfilePermission>> Handle(ReadProfilePermissionService request,
            CancellationToken cancellationToken)
        {
            var profilePermissions = await _profilePermissionRepository.ListAsync(
                new ProfilePermissionsByIds(request.Ids), cancellationToken);

            return profilePermissions;
        }
    }
}