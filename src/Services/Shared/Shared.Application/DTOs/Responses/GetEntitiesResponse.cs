namespace Shared.Application.DTOs.Responses;

public class GetEntitiesResponse<T>
{
    public IReadOnlyCollection<T> Entities { get; }
    public PaginationResponse Pagination { get; }

    public GetEntitiesResponse(IReadOnlyCollection<T> entities, PaginationResponse pagination)
    {
        Entities = entities;
        Pagination = pagination;
    }
}