using Ardalis.Specification;

namespace Shared.Domain.Interfaces.Repositories;

public interface IReadRepository<T> : IReadRepositoryBase<T> where T : class
{
}