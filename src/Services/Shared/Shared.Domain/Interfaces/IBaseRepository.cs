using Shared.Domain.SeedWork;

namespace Shared.Domain.Interfaces;

public interface IBaseRepository<T> where T : BaseEntity
{
    IUnitOfWork UnitOfWork { get; }
}