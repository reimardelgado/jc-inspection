using Ardalis.Specification;

namespace Backend.Application.Specifications.ProfilePermissionsSpecs
{
    public sealed class ProfilePermissionsByIds : Specification<ProfilePermission>
    {
        public ProfilePermissionsByIds(ICollection<Guid> ids)
        {
            Query
                .Include(permission => permission.Permission)
                .Where(permission => ids.Contains(permission.ProfileId));
        }
    }
}