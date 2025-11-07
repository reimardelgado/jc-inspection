using Ardalis.Specification;

namespace Backend.Application.Specifications.MemberSpecs
{
    public class MobileIdActiveUserSpec : Ardalis.Specification.Specification<User>
    {
        public MobileIdActiveUserSpec(string? status, string? notificationType, string? category)
        {
            Query.Where(user => user.MobileId != null);

            if (!string.IsNullOrEmpty(status))
            {
                Query.Where(user => user.Status.ToLower().Equals(status.ToLower()));
            }

        }
    }
}