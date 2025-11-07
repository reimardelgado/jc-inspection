using Ardalis.Specification;
using Shared.Domain.Specification;

namespace Backend.Application.Specifications.PhotoSpecs
{
    public sealed class PhotoSpec : Ardalis.Specification.Specification<Photo>, ISingleResultSpecification
    {
        public PhotoSpec(Guid inspectionId, Guid sectionId)
        {
            Query
                .Include(x => x.Inspection)
                .Where(x => x.InspectionId.Equals(inspectionId)
                            && x.SectionId.Equals(sectionId)
                            && x.Status != StatusEnum.Deleted);
        }

        public PhotoSpec(Guid? id)
        {
            Query
                .Include(x => x.Inspection)
                .Where(x => x.Status != StatusEnum.Deleted);
            if (id != null)
            {
                Query.Where(x => x.Id.Equals(id));    
            }
        }

        public PhotoSpec()
        {
            Query.Where(x => x.Status != StatusEnum.Deleted);
        }
    }
}