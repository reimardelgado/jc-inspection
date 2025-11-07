using Ardalis.Specification;

namespace Backend.Application.Specifications.UserByEmailSpecs
{
    public sealed class UserByEmailSpec : Ardalis.Specification.Specification<User>, ISingleResultSpecification
    {
        public UserByEmailSpec(string email)
        {
                Query.Where(usr => usr.Email == email && usr.Status == UserState.Active);
        }
    }
}