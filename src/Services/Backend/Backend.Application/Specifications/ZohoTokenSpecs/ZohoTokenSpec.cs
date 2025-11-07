using Ardalis.Specification;
using Shared.Domain.Specification;

namespace Backend.Application.Specifications.ZohoTokenSpecs
{
    public sealed class ZohoTokenSpec : Ardalis.Specification.Specification<ZohoToken>, ISingleResultSpecification
    {
        public ZohoTokenSpec()
        {
            Query.Where(x => x.Status != StatusEnum.Deleted);
            Query.OrderByDescending(x => x.CreatedAt);
        }
    }
}