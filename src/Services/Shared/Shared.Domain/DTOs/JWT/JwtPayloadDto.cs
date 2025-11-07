namespace Shared.Domain.DTOs.JWT;

public class JwtPayloadDto
{
    public JwtPayloadDto(Guid id, string fullname, string scope, ICollection<string>? permissions)
    {
        Id = id;
        Fullname = fullname;
        Scope = scope;
        Permissions = permissions;
    }

    public Guid Id { get; }
    public string Fullname { get; }
    public string Scope { get; }
    public ICollection<string>? Permissions { get; }
}

public class JwtPayloadPermissionsDto
{
    public JwtPayloadPermissionsDto(List<string> program, List<string> global)
    {
        Program = program;
        Global = global;
    }

    public List<string> Program { get; }
    public List<string> Global { get; }
}