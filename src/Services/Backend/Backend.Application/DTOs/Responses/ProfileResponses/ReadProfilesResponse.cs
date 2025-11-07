namespace Backend.Application.DTOs.Responses.ProfileResponses
{
    public record ReadProfilesResponse(Guid Id, string name, string description, List<ReadProfilePermissionDto> permissions)
    {
        public static ReadProfilesResponse FromEntity(Profile profile)
        {
            List<ReadProfilePermissionDto> permissions = new();

            var profilePermissions = profile.ProfilePermissions.Any();
            if (profilePermissions)
            {
                permissions = profile.ProfilePermissions
                    .Select(x => ReadProfilePermissionDto.FromEntity(x.Permission))
                    .ToList();
            }

            return new ReadProfilesResponse(profile.Id, profile.Name, profile.Description, permissions);
        }
    }

    public record ReadProfilesPermissionDto(Guid Id, string Code, string Description, string ResourceCode)
    {
        public static ReadProfilesPermissionDto FromEntity(Permission permission)
        {
            return new ReadProfilesPermissionDto(permission.Id, permission.Code, permission.Description,
                permission.ResourceCode);
        }
    }
}