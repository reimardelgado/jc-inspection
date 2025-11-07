using Ardalis.Specification.EntityFrameworkCore;
using Shared.Domain.Interfaces.Repositories;

namespace Backend.Infrastructure.Repositories;

/// <summary>
/// "There's some repetition here - couldn't we have some the sync methods call the async?"
/// https://blogs.msdn.microsoft.com/pfxteam/2012/04/13/should-i-expose-synchronous-wrappers-for-asynchronous-methods/
/// </summary>
/// <typeparam name="T"></typeparam>
public class EfRepository<T> : RepositoryBase<T>, IReadRepository<T>, IRepository<T> where T : class
{
    public EfRepository(BackendDbContext dbContext) : base(dbContext)
    {
    }
}