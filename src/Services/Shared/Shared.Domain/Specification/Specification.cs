namespace Shared.Domain.Specification;

public class Specification<T>
    where T : class
{
    public Specification(FilterField<T> filterField, FilterOperator @operator,
        FilterComparer comparer, object value)
    {
        FilterField = filterField;
        Operator = @operator;
        Comparer = comparer;
        Value = value;
    }

    public FilterField<T> FilterField { get; }
    public FilterOperator Operator { get; }
    public FilterComparer Comparer { get; }
    public object Value { get; }
}