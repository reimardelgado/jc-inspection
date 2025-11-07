using Ardalis.Specification;
using Shared.Domain.Specification;

namespace Backend.Application.Specifications.InspectionSpecs
{
    public sealed class InspectionSpec : Ardalis.Specification.Specification<Inspection>, ISingleResultSpecification
    {
        public InspectionSpec(Guid id)
        {
            Query
                .Include(x => x.Inspector)
                .Include(x => x.FormTemplate)
                .Where(x => x.Id.Equals(id) && x.InspectionStatus != InspectionStatusEnum.Deleted);
        }

        public InspectionSpec(Guid? id, string? name)
        {
            Query
                .Include(x => x.Inspector)
                .Include(x => x.FormTemplate)
                .Where(x => x.InspectionStatus != InspectionStatusEnum.Deleted);
            if (id != null)
            {
                Query.Where(x => x.InspectorId == id
                                 && x.InspectionStatus == InspectionStatusEnum.Created
                                 || x.InspectionStatus == InspectionStatusEnum.InAction
                                 || x.InspectionStatus == InspectionStatusEnum.Rejected);
            }

            if (!string.IsNullOrEmpty(name))
                Query.Where(x => x.Name.ToLower().Equals(name.ToLower()));

            Query.OrderBy(x => x.CreatedAt);
        }

        public InspectionSpec(string? queryParam, bool isPagingEnabled, int page, int pageSize)
        {
            Query
                .Include(x => x.Inspector)
                .Include(x => x.FormTemplate)
                .Where(x => x.InspectionStatus != InspectionStatusEnum.Deleted);


            if (!string.IsNullOrEmpty(queryParam))
                Query.Where(x => x.Name.ToLower().Contains(queryParam.ToLower()));

            if (isPagingEnabled)
                Query
                    .OrderBy(user => user.CreatedAt)
                    .Skip(PaginationHelper.CalculateSkip(pageSize, page))
                    .Take(PaginationHelper.CalculateTake(pageSize));
        }

        public InspectionSpec()
        {
            Query
                .Include(x => x.Inspector)
                .Include(x => x.FormTemplate)
                .Where(x => x.InspectionStatus != InspectionStatusEnum.Deleted);
        }
    }
}