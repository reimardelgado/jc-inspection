using Ardalis.Specification;

namespace Backend.Application.Specifications.UserProfileSpecs
{
    public sealed class UserProfileSpec : Ardalis.Specification.Specification<UserProfile>, ISingleResultSpecification
    {
        public UserProfileSpec(Guid? applicationId, Guid? profileId)
        {
            if (applicationId != null)
            {
                Query.Where(profile => profile.UserId == applicationId);
            }

            if (profileId != null)
            {
                Query.Where(profile => profile.ProfileId == profileId);
            }

            Query.OrderBy(profile => profile.CreatedAt);
        }
    }
}