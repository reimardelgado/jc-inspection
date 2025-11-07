using Ardalis.Specification;
using Shared.Domain.Specification;

namespace Backend.Application.Specifications.InspectionResultSpecs
{
    public sealed class InspectionResultByResultSpec : Ardalis.Specification.Specification<InspectionResult>, ISingleResultSpecification
    {
        public InspectionResultByResultSpec(Guid inspectionId, Guid formTemplateId)
        {
            Query
                .Where(x => x.InspectionId.Equals(inspectionId) 
                            && x.FormTemplateId.Equals(formTemplateId)
                            && x.InspectionResultStatus != InspectionStatusEnum.Deleted);
        }
    }
}