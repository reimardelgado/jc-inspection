using Ardalis.Specification;
using Shared.Domain.Specification;

namespace Backend.Application.Specifications.CatalogSpecs
{
    public sealed class CatalogSpec : Ardalis.Specification.Specification<Catalog>, ISingleResultSpecification
    {
        public CatalogSpec(Guid id)
        {
            Query.Where(x => x.Id.Equals(id) && x.Status != StatusEnum.Deleted);
        }

        public CatalogSpec(Guid? id, string? name)
        {
            Query.Where(x => x.Status != StatusEnum.Deleted);
            if (id != null)
            {
                Query.Where(x => x.Id == id);
            }
            if (!string.IsNullOrEmpty(name))
                Query.Where(x => x.Name.ToLower().Equals(name.ToLower()));

            Query.OrderBy(x => x.CreatedAt);
        }

        public CatalogSpec(string? queryParam, bool isPagingEnabled, int page, int pageSize)
        {
            Query
                .Where(x => x.Status != StatusEnum.Deleted);


            if (!string.IsNullOrEmpty(queryParam))
                Query.Where(x => x.Name.ToLower().Contains(queryParam.ToLower()));

            if (isPagingEnabled)
                Query
                    .OrderBy(user => user.CreatedAt)
                    .Skip(PaginationHelper.CalculateSkip(pageSize, page))
                    .Take(PaginationHelper.CalculateTake(pageSize));
        }

        public CatalogSpec()
        {
            Query.Where(x => x.Status != StatusEnum.Deleted);

        }
    }
}