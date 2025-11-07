using Ardalis.Specification;
using Shared.Domain.Specification;

namespace Backend.Application.Specifications.CatalogValueSpecs
{
    public sealed class CatalogValueSpec : Ardalis.Specification.Specification<CatalogValue>, ISingleResultSpecification
    {
        public CatalogValueSpec(Guid catalogIg)
        {
            Query.Where(x => x.CatalogId.Equals(catalogIg) && x.Status != StatusEnum.Deleted);
        }

        public CatalogValueSpec(Guid? id, string? name)
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

        public CatalogValueSpec(Guid? catalogId, string? queryParam, bool isPagingEnabled, int page, int pageSize)
        {
            Query
                .Where(x => x.Status != StatusEnum.Deleted);

            if (catalogId != null)
            {
                Query.Where(x => x.CatalogId == catalogId);
            }

            if (!string.IsNullOrEmpty(queryParam))
                Query.Where(x => x.Name.ToLower().Contains(queryParam.ToLower()));

            if (isPagingEnabled)
                Query
                    .OrderBy(user => user.CreatedAt)
                    .Skip(PaginationHelper.CalculateSkip(pageSize, page))
                    .Take(PaginationHelper.CalculateTake(pageSize));
        }

        public CatalogValueSpec()
        {
            Query.Where(x => x.Status != StatusEnum.Deleted);

        }
    }
}