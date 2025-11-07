namespace Shared.Domain.Models;

public class EntityResponse
{
    protected ResultStatus Status { get; init; } = ResultStatus.Ok;
    public bool IsSuccess => Status == ResultStatus.Ok;

    public static EntityResponse<T> Success<T>(T value) => new(value);
}

public class EntityResponse<T> : EntityResponse
{
    public T? Value { get; }

    public IEnumerable<string> Errors { get; private init; } = new List<string>();

    public EntityResponse(T value)
    {
        Value = value;
    }

    private EntityResponse(ResultStatus status)
    {
        Status = status;
    }

    public static implicit operator T?(EntityResponse<T> result) => result.Value;
    public static implicit operator EntityResponse<T>(T value) => Success(value);

    public static EntityResponse<T> Error(params string[] errorMessages)
    {
        return new EntityResponse<T>(ResultStatus.Error) { Errors = errorMessages };
    }

    public static EntityResponse<T> Error<TK>(EntityResponse<TK> result)
    {
        return new EntityResponse<T>(ResultStatus.Error) { Errors = result.Errors };
    }
}