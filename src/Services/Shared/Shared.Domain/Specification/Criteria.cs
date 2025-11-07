namespace Shared.Domain.Specification;

public class Criteria<T> where T : class
{
    public Criteria(List<Filter> filters, List<OrderBy> orderByList, int? page, int? pageSize)
    {
        Specifications = GetSpecifications(filters);
        Orders = GetOrders(orderByList);
        Page = page ?? 0;
        PageSize = pageSize ?? 100;
    }

    public List<Specification<T>> Specifications { get; }
    public List<Order<T>> Orders { get; }
    public int Page { get; private set; }
    public int PageSize { get; private set; }

    private List<Specification<T>> GetSpecifications(List<Filter> filters)
    {
        var specifications = new List<Specification<T>>();

        foreach (var filter in filters)
        {
            var specification =
                new Specification<T>(new FilterField<T>(filter.Field),
                    filter.Operator, filter.Comparer, filter.Value);

            specifications.Add(specification);
        }

        return specifications;
    }

    private List<Order<T>> GetOrders(List<OrderBy> orderByList)
    {
        var orders = new List<Order<T>>();

        foreach (var orderBy in orderByList)
        {
            var order =
                new Order<T>(new OrderField<T>(orderBy.Field),
                    orderBy.OrderType);

            orders.Add(order);
        }

        return orders;
    }

    public void SetPage(int value)
    {
        Page = value;
    }

    public void SetPageSize(int value)
    {
        PageSize = value;
    }
}