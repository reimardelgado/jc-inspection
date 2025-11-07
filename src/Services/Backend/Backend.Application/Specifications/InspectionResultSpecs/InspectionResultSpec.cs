using Ardalis.Specification;
using Shared.Domain.Specification;

namespace Backend.Application.Specifications.InspectionResultSpecs
{
    public sealed class InspectionResultSpec : Ardalis.Specification.Specification<InspectionResult>, ISingleResultSpecification
    {
        public InspectionResultSpec(Guid id)
        {
            Query
                .Where(x => x.Id.Equals(id) && x.InspectionResultStatus != InspectionStatusEnum.Deleted);
        }

        public InspectionResultSpec(Guid? id, string? name)
        {
            Query
                .Where(x => x.InspectionResultStatus != InspectionStatusEnum.Deleted);
            if (id != null)
            {
                Query.Where(x => x.Id == id);
            }
            if (!string.IsNullOrEmpty(name))
                Query.Where(x => x.SectionName.ToLower().Equals(name.ToLower()));

            Query.OrderBy(x => x.CreatedAt);
        }

        public InspectionResultSpec(string? queryParam, bool isPagingEnabled, int page, int pageSize)
        {
            Query
                .Where(x => x.InspectionResultStatus != InspectionStatusEnum.Deleted);


            if (!string.IsNullOrEmpty(queryParam))
                Query.Where(x => x.SectionName.ToLower().Contains(queryParam.ToLower()));

            if (isPagingEnabled)
                Query
                    .OrderBy(user => user.CreatedAt)
                    .Skip(PaginationHelper.CalculateSkip(pageSize, page))
                    .Take(PaginationHelper.CalculateTake(pageSize));
        }

        public InspectionResultSpec()
        {
            Query
                .Where(x => x.InspectionResultStatus != InspectionStatusEnum.Deleted);

        }
    }
}