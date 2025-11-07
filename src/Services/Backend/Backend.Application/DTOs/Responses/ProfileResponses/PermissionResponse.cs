namespace Backend.Application.DTOs.Responses.ProfileResponses
{
    public record PermissionResponse(Guid Id, string Code, string Description, string ResourceCode, string Type)
    {
        public static List<PermissionResponse> FromEntity(List<Permission> permissions)
        {
            return permissions.Select(i => new PermissionResponse(i.Id, i.Code, i.Description, i.ResourceCode, i.Type))
                .ToList();
        }
    }
}