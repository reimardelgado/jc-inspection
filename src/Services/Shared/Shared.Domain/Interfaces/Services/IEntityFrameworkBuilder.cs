using System.Linq.Expressions;
using Shared.Domain.Specification;

namespace Shared.Domain.Interfaces.Services;

public interface IEntityFrameworkBuilder<T>
    where T : class
{
    Expression<Func<T, bool>> GetWhereExpression(List<Specification<T>> specifications);

    Task<List<T>> ToListOrderedPagedValues(IQueryable<T> query, List<Order<T>> orders,
        int page, int pageSize);
}