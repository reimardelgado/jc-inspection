namespace Backend.Application.DTOs.Responses.ProfileResponses
{
    public class ReadProfileResponse
    {
        public Guid Id { get; }
        public string Name { get; }
        public string? Description { get; }
        public List<ReadProfilePermissionDto> Permissions { get; } = new();

        public ReadProfileResponse(Guid id, string name, string? description, List<ReadProfilePermissionDto> permissions)
        {
            Id = id;
            Name = name;
            Description = description;
            Permissions = permissions;
        }
    
        public static ReadProfileResponse FromEntity(Profile profile)
         {
            List<ReadProfilePermissionDto> permissions = new();
            if (profile.ProfilePermissions.Any())
            {
                permissions = profile.ProfilePermissions
                    .Select(permission => ReadProfilePermissionDto.FromEntity(permission.Permission))
                    .ToList();
            }

            return new ReadProfileResponse(profile.Id, profile.Name, profile.Description, permissions);
        }
    }

    public record ReadProfilePermissionDto(Guid Id, string Code, string Description, string ResourceCode)
    {
        public static ReadProfilePermissionDto FromEntity(Permission permission)
        {
            return new ReadProfilePermissionDto(permission.Id, permission.Code, permission.Description,
                permission.ResourceCode);
        }
    }
}