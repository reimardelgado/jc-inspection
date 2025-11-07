namespace Shared.Application.Utils;

public class QueryResponsePaginationModel<T>
{
    public readonly IReadOnlyCollection<T> Collection;
    public readonly PaginationResponse PaginationResponse;

    public QueryResponsePaginationModel(IReadOnlyCollection<T> collection, PaginationResponse paginationResponse)
    {
        Collection = collection;
        PaginationResponse = paginationResponse;
    }
}