using Ardalis.Specification;

namespace Backend.Application.Specifications.UserGlobalPermissionSpecs
{
    public sealed class UserGlobalPermissionByUserIdSpec : Specification<UserGlobalPermission>
    {
        public UserGlobalPermissionByUserIdSpec(Guid userId)
        {
            Query
                .Where(permission => permission.UserId == userId);
        }
    }
}