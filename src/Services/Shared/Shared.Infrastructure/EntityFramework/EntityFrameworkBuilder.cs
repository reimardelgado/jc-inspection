using System.Collections;
using System.Linq.Expressions;
using Shared.Domain.Interfaces.Services;
using Shared.Domain.Specification;

namespace Shared.Infrastructure.EntityFramework;

public class EntityFrameworkBuilder<T> : IEntityFrameworkBuilder<T>
    where T : class
{
    #region Select Order

    /// <summary>
    ///     Creates lambda expression dynamically based on order expression
    /// </summary>
    private IOrderedQueryable<T> ApplyOrder(
        IQueryable<T> source,
        string propertyName,
        string methodName)
    {
        try
        {
            var entity = Expression.Parameter(typeof(T));
            var property = Expression.Property(entity, propertyName);

            var delegateType = typeof(Func<,>).MakeGenericType(typeof(T), property.Type);
            var lambda = Expression.Lambda(delegateType, property, entity);

            var result = typeof(Queryable).GetMethods().Single(
                    method => method.Name == methodName
                              && method.IsGenericMethodDefinition
                              && method.GetGenericArguments().Length == 2
                              && method.GetParameters().Length == 2)
                .MakeGenericMethod(typeof(T), property.Type)
                .Invoke(null, new object[] { source, lambda });

            return (IOrderedQueryable<T>)result;
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "There was an error selecting order for {propertyName}",
                propertyName);

            throw new Exception("There was en error ordering information with " +
                                $"propertyName {propertyName}",
                exception);
        }
    }

    #endregion

    #region OrderEnumerator

    internal static class OrderByType
    {
        internal static string OrderBy => "OrderBy";
        internal static string OrderByDescending => "OrderByDescending";
        internal static string ThenBy => "ThenBy";
        internal static string ThenByDescending => "ThenByDescending";
    }

    #endregion

    #region Constructor & properties

    private readonly ILogger<EntityFrameworkBuilder<T>> _logger;

    public EntityFrameworkBuilder(ILogger<EntityFrameworkBuilder<T>> logger)
    {
        _logger = logger;
    }

    #endregion

    #region Public methods

    /// <summary>
    ///     Builds dynamically where ef expression based on a list of specifications
    /// </summary>
    // Based on: https://stackoverflow.com/questions/8315819/expression-lambda-and-query-generation-at-runtime-simplest-where-example
    public Expression<Func<T, bool>> GetWhereExpression(List<Specification<T>> specifications)
    {
        if (specifications == null || !specifications.Any()) return null;

        Expression<Func<T, bool>> result = null;

        foreach (var specification in specifications)
        {
            // For first specification
            if (result == null)
            {
                result = SelectOperator(specification);
                continue;
            }

            // For more than one specification, apply && or || based on comparer
            if (specification.Comparer.Equals(FilterComparer.And))
                result = And(result, SelectOperator(specification));
            else
                result = Or(result, SelectOperator(specification));
        }

        return result;
    }

    // https://stackoverflow.com/questions/41244/dynamic-linq-orderby-on-ienumerablet-iqueryablet
    public async Task<List<T>> ToListOrderedPagedValues(IQueryable<T> query, List<Order<T>> orders,
        int page, int pageSize)
    {
        // Do not order by asc or desc if there is no order parameters
        if (orders == null || !orders.Any())
            return await query
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToListAsync();

        IQueryable<T> result = null;

        foreach (var order in orders)
        {
            // For first order
            if (result == null)
            {
                if (order.OrderType.Equals(OrderType.Asc))
                    result = ApplyOrder(query, order.OrderField.FieldName, OrderByType.OrderBy);
                else
                    result = ApplyOrder(query, order.OrderField.FieldName, OrderByType.OrderByDescending);

                continue;
            }

            if (order.OrderType.Equals(OrderType.Asc))
                result = ApplyOrder(result, order.OrderField.FieldName, OrderByType.ThenBy);
            else
                result = ApplyOrder(result, order.OrderField.FieldName, OrderByType.ThenByDescending);
        }

        return await result
            .Skip(page * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    #endregion

    #region Select Operator

    /// <summary>
    ///     Build lambda expression based on specification
    /// </summary>
    /// <param name="specification"></param>
    /// <returns></returns>
    private Expression<Func<T, bool>> SelectOperator(Specification<T> specification)
    {
        try
        {
            return GetOperationExpression(specification.Value, specification.FilterField.FieldName,
                specification.Operator.OperatorExpression, specification.Operator.Name,
                specification.Operator.IsNegative);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "There was an error selecting operator for {@specification}",
                specification);

            throw new Exception("There was en error filtering information with " +
                                $"filter {specification.FilterField.FieldName}",
                exception);
        }
    }

    /// <summary>
    ///     Creates lambda expression dynamically based on operator expression
    /// </summary>
    private Expression<Func<T, bool>> GetOperationExpression(object value, string propertyName,
        Func<Expression, Expression, BinaryExpression>? operatorExpression, string methodName,
        bool isNegative)
    {
        ParameterExpression entity;
        MemberExpression property;
        ConstantExpression constantValue;
        Expression<Func<T, bool>> lambda;

        //Set lamba expression parameters
        SetExpressionParameters(value, propertyName, out entity, out property, out constantValue);

        //Set operator
        lambda = GetMethodExpression(operatorExpression, methodName, entity, property, constantValue, value);

        if (isNegative) lambda = Expression.Lambda<Func<T, bool>>(Expression.Not(lambda.Body), entity);

        return lambda;
    }

    /// <summary>
    ///     Returns lambda expression based on method definition
    /// </summary>
    private static Expression<Func<T, bool>> GetMethodExpression(
        Func<Expression, Expression, BinaryExpression>? operatorExpression,
        string methodName, ParameterExpression entity, MemberExpression property,
        ConstantExpression constantValue, object value)
    {
        Expression<Func<T, bool>> lambda = s => s.Equals("");

        //For methods without an Expression, search for method
        if (operatorExpression == null)
        {
            MethodInfo method;
            MethodCallExpression callExpression;

            if (methodName.Contains("Contains")) methodName = "Contains";

            if (value is IList)
            {
                var listType = typeof(List<>).MakeGenericType(property.Type);
                method = listType.GetMethod(methodName, new[] { property.Type });

                if (method != null)
                {
                    callExpression = Expression.Call(constantValue, method, property);
                    lambda = Expression.Lambda<Func<T, bool>>(callExpression, entity);
                }
            }
            else if (value is string valueStr)
            {
                var callExpressionToUpper = Expression.Call(property, "ToUpper", null);
                callExpression = Expression.Call(callExpressionToUpper, methodName, null,
                    Expression.Constant(valueStr.ToUpper()));
                lambda = Expression.Lambda<Func<T, bool>>(callExpression, entity);
            }
            else
            {
                method = property.Type.GetMethod(methodName, new[] { property.Type });
                if (method != null)
                {
                    callExpression = Expression.Call(property, method, constantValue);
                    lambda = Expression.Lambda<Func<T, bool>>(callExpression, entity);
                }
            }
        }
        //Use expression
        else
        {
            var @operator = operatorExpression(property, constantValue);
            lambda = Expression.Lambda<Func<T, bool>>(@operator, entity);
        }

        return lambda;
    }

    #endregion

    #region Other methods

    /// <summary>
    ///     Build lambda expression parameters
    /// </summary>
    private void SetExpressionParameters(object value, string propertyName,
        out ParameterExpression entity, out MemberExpression property,
        out ConstantExpression constantValue)
    {
        entity = Expression.Parameter(typeof(T));
        property = Expression.Property(entity, propertyName);

        if (value is IList)
        {
            var listType = typeof(List<>).MakeGenericType(property.Type);
            constantValue = Expression.Constant(value, listType);
        }
        else
        {
            value = CastValue(value, property.Type);
            constantValue = Expression.Constant(value, property.Type);
        }
    }

    /// <summary>
    ///     Cast values based on a type
    /// </summary>
    private object CastValue(object value, Type type)
    {
        if (type.Equals(typeof(Guid)) || type.Equals(typeof(Guid?)))
        {
            var parsed = Guid.TryParse(value?.ToString(), out var output);
            return parsed ? output : null;
        }

        if (type.Equals(typeof(DateTime)) || type.Equals(typeof(DateTime?)))
        {
            return value;
        }

        //T t = (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFromInvariantString(text);
        return Convert.ChangeType(value, type);
    }

    #endregion

    #region Compare methods

    /// <summary>
    ///     Joins two expression using && comparer
    /// </summary>
    private Expression<Func<T, bool>> And(Expression<Func<T, bool>> left, Expression<Func<T, bool>> right)
    {
        var paramExpr = Expression.Parameter(typeof(T));
        var exprBody = Expression.AndAlso(left.Body, right.Body);
        exprBody = (BinaryExpression)new ParameterReplacer(paramExpr).Visit(exprBody);
        var finalExpr = Expression.Lambda<Func<T, bool>>(exprBody, paramExpr);

        return finalExpr;
    }

    /// <summary>
    ///     Joins two expression using && comparer
    /// </summary>
    private Expression<Func<T, bool>> Or(Expression<Func<T, bool>> left, Expression<Func<T, bool>> right)
    {
        var paramExpr = Expression.Parameter(typeof(T));
        var exprBody = Expression.OrElse(left.Body, right.Body);
        exprBody = (BinaryExpression)new ParameterReplacer(paramExpr).Visit(exprBody);
        var finalExpr = Expression.Lambda<Func<T, bool>>(exprBody, paramExpr);

        return finalExpr;
    }

    #endregion
}