using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Shared.Infrastructure.Security;

public class JwtAuthorizationActionFilter : IAuthorizationFilter
{
    #region Public Methods

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        if (!string.IsNullOrWhiteSpace(environment) && environment.Equals("Local"))
        {
            return;
        }

        var authorizeAttributes = context.ActionDescriptor
            .EndpointMetadata
            .OfType<JwtAuthorizeAttribute>()
            .ToList();

        if (authorizeAttributes.Count == 0) return;

        ValidateAuthorization(context, authorizeAttributes);
    }

    #endregion

    #region Constructor & Properties

    private readonly IHttpContextAccessor _httpContextAccessor;

    public JwtAuthorizationActionFilter(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    #endregion

    #region Private Methods

    private ICollection<string> GetPermissions()
    {
        var permissionsType = _httpContextAccessor.HttpContext?.User.Claims
            .Where(t => t.Type.Equals("permissions"))
            .ToList();

        if (permissionsType is null || !permissionsType.Any())
        {
            return new List<string>();
        }

        var permissions = permissionsType.Select(t => t.Value)
            .ToList();

        // Permission that receive in claims
        return permissions;
    }

    private string? GetScope()
    {
        return _httpContextAccessor.HttpContext?.User.Claims
            .Where(t => t.Type.Equals("scope"))
            .Select(t => t.Value.ToString())
            .FirstOrDefault();
    }

    private void ValidateAuthorization(AuthorizationFilterContext context,
        IEnumerable<JwtAuthorizeAttribute> authorizeAttributes)
    {
        var scope = GetScope();
        var permissions = GetPermissions();

        // Verify the Authorization
        foreach (var jwtAuthorizeAttribute in authorizeAttributes)
            if (scope != null && scope.Equals(jwtAuthorizeAttribute.Scope.ToString(),
                    StringComparison.InvariantCultureIgnoreCase))
            {
                if (jwtAuthorizeAttribute.Permissions.Count == 0) return;

                var isPermissionFound = permissions.Any(t => jwtAuthorizeAttribute.Permissions.Contains(t));
                if (isPermissionFound) return;
            }

        context.Result = new UnauthorizedResult();
    }

    #endregion
}