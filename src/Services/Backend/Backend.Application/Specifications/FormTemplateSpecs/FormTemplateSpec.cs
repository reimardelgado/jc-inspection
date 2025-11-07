using Ardalis.Specification;
using Shared.Domain.Specification;

namespace Backend.Application.Specifications.FormTemplateSpecs
{
    public sealed class FormTemplateSpec : Ardalis.Specification.Specification<FormTemplate>, ISingleResultSpecification
    {
        public FormTemplateSpec(Guid id)
        {
            Query.Where(x => x.Id.Equals(id) && x.Status != StatusEnum.Deleted);
        }

        public FormTemplateSpec(Guid? id, string? name, string? description)
        {
            Query.Where(x => x.Status != StatusEnum.Deleted);
            if (id != null)
            {
                Query.Where(x => x.Id == id);
            }
            if (!string.IsNullOrEmpty(name))
                Query.Where(x => x.Name!.ToLower().Equals(name.ToLower()));
            
            if (!string.IsNullOrEmpty(description))
                Query.Where(x => x.Description!.ToLower().Equals(description.ToLower()));

            Query.OrderBy(x => x.CreatedAt);
        }

        public FormTemplateSpec(string? queryParam, bool isPagingEnabled, int page, int pageSize)
        {
            Query
                .Where(x => x.Status != StatusEnum.Deleted);


            if (!string.IsNullOrEmpty(queryParam))
                Query.Where(x => x.Name!.ToLower().Contains(queryParam.ToLower())
                || x.Description!.ToLower().Contains(queryParam.ToLower()));

            if (isPagingEnabled)
                Query
                    .OrderBy(user => user.CreatedAt)
                    .Skip(PaginationHelper.CalculateSkip(pageSize, page))
                    .Take(PaginationHelper.CalculateTake(pageSize));
        }

        public FormTemplateSpec()
        {
            Query.Where(x => x.Status != StatusEnum.Deleted);

        }
    }
}