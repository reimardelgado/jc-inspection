namespace Shared.Domain.Specification;

public interface ISpecificationBaseRepository<T>
    where T : class
{
    Task<IReadOnlyCollection<T>> Find(Criteria<T> criteria);

    Task<int> Count(List<Specification<T>> specifications);
}