using Ardalis.Specification;
using Shared.Domain.Specification;

namespace Backend.Application.Specifications.ItemSectionSpecs
{
    public sealed class ItemSectionSpec : Ardalis.Specification.Specification<ItemSection>, ISingleResultSpecification
    {
        public ItemSectionSpec(Guid formTemplateId)
        {
            Query.Where(x => x.FormTemplateId.Equals(formTemplateId) && x.Status != StatusEnum.Deleted);
        }

        public ItemSectionSpec(Guid? id, string? name, Guid? formTemplateId)
        {
            Query.Where(x => x.Status != StatusEnum.Deleted);
            if (id != null)
            {
                Query.Where(x => x.Id == id);
            }
            if (!string.IsNullOrEmpty(name))
                Query.Where(x => x.Name!.ToLower().Equals(name.ToLower()));
            
            if (formTemplateId != null)
                Query.Where(x => x.FormTemplateId!.Equals(formTemplateId));

            Query.OrderBy(x => x.CreatedAt);
        }

        public ItemSectionSpec(Guid? formTemplateId, string? queryParam, bool isPagingEnabled, int page, int pageSize)
        {
            Query
                .Where(x => x.Status != StatusEnum.Deleted);

            if (formTemplateId != null)
                Query.Where(x => x.FormTemplateId!.Equals(formTemplateId));

            if (!string.IsNullOrEmpty(queryParam))
                Query.Where(x => x.Name!.ToLower().Contains(queryParam.ToLower()));

            if (isPagingEnabled)
                Query
                    .OrderBy(user => user.CreatedAt)
                    .Skip(PaginationHelper.CalculateSkip(pageSize, page))
                    .Take(PaginationHelper.CalculateTake(pageSize));
        }

        public ItemSectionSpec()
        {
            Query.Where(x => x.Status != StatusEnum.Deleted);

        }
    }
}