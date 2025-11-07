using Ardalis.Specification;
using Shared.Domain.Specification;

namespace Backend.Application.Specifications.ItemSpecs
{
    public sealed class ItemSpec : Ardalis.Specification.Specification<Item>, ISingleResultSpecification
    {
        public ItemSpec(Guid formTemplateId)
        {
            Query
                .Include(x => x.ItemSection)
                .Include(x => x.FormTemplate)
                .Include(x => x.Catalog)
                .Where(x => x.FormTemplateId.Equals(formTemplateId) && x.Status != StatusEnum.Deleted);
        }
        
        public ItemSpec(Guid formTemplateId, Guid itemSectionId)
        {
            Query
                .Include(x => x.ItemSection)
                .Include(x => x.FormTemplate)
                .Include(x => x.Catalog)
                .Where(x => x.FormTemplateId.Equals(formTemplateId)
                            && x.ItemSectionId.Equals(itemSectionId) && x.Status != StatusEnum.Deleted);
        }

        public ItemSpec(Guid? formTemplateId, Guid? itemSectionId, string? name)
        {
            Query
                .Include(x => x.ItemSection)
                .Include(x => x.FormTemplate)
                .Include(x => x.Catalog)
                .Where(x => x.Status != StatusEnum.Deleted);
            if (formTemplateId != null)
            {
                Query.Where(x => x.FormTemplateId == formTemplateId);
            }
            if (itemSectionId != null)
            {
                Query.Where(x => x.ItemSectionId == itemSectionId);
            }
            if (!string.IsNullOrEmpty(name))
                Query.Where(x => x.Name!.ToLower().Equals(name.ToLower()));
            
            if (formTemplateId != null)
                Query.Where(x => x.FormTemplateId!.Equals(formTemplateId));

            Query.OrderBy(x => x.CreatedAt);
        }

        public ItemSpec(Guid? formTemplateId, string? queryParam, bool isPagingEnabled, int page, int pageSize)
        {
            Query
                .Include(x => x.ItemSection)
                .Include(x => x.FormTemplate)
                .Include(x => x.Catalog)
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

        public ItemSpec()
        {
            Query
                .Include(x => x.ItemSection)
                .Include(x => x.FormTemplate)
                .Include(x => x.Catalog)
                .Where(x => x.Status != StatusEnum.Deleted);

        }
    }
}