using Shared.Domain.Specification;

namespace Shared.Domain.Utils;

public static class FilterPaginationUtils
{
    public static List<Filter> ConstructFilters(string[] filters, List<Filter>? customFilters = null)
    {
        var result = new List<Filter>();
        if (!filters.Any())
        {
            if (customFilters != null)
            {
                result.AddRange(customFilters);
            }

            return result;
        }

        foreach (var filterString in filters)
        {
            var parameterList = filterString.Split(',').ToList();
            if (parameterList.Count != 4)
            {
                throw new ApplicationException("Filters are not well formatted. " +
                                               "They should follow this format: \"field,value,operator,comparer\". " +
                                               $"Possible values for operator are: {FilterOperator.List()}. " +
                                               $"Possible values for comparer are: {FilterComparer.List()}. ");
            }

            if (customFilters != null && customFilters.Any(f => f.Field.Equals(parameterList[0])))
            {
                continue;
            }

            var filter = new Filter(parameterList[0], FilterOperator.FromName(parameterList[2]),
                FilterComparer.FromName(parameterList[3]), parameterList[1]);

            result.Add(filter);
        }

        if (customFilters != null)
        {
            result.AddRange(customFilters);
        }

        return result;
    }

    public static List<OrderBy> ConstructOrders(string[]? orders)
    {
        var result = new List<OrderBy>();

        if (orders == null || !orders.Any())
        {
            return result;
        }

        foreach (var orderString in orders)
        {
            var parameters = orderString.Split(',').ToList();
            if (parameters.Count != 2)
            {
                throw new ApplicationException("Orders are not well formatted. " +
                                               "They should follow this format: \"field,orderType\". " +
                                               $"Possible values for orderType are: {OrderType.List()}. ");
            }

            var orderBy = new OrderBy(parameters[0], OrderType.FromName(parameters[1]));
            result.Add(orderBy);
        }

        return result;
    }
}