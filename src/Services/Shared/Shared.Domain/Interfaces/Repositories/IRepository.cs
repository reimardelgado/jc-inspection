using Ardalis.Specification;

namespace Shared.Domain.Interfaces.Repositories;

public interface IRepository<T> : IRepositoryBase<T> where T : class
{
}