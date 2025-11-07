using Microsoft.Extensions.Configuration;
using Shared.Domain.DTOs.JWT;
using Shared.Domain.Interfaces.Services;

namespace Shared.Infrastructure.Security;

public class JwtTokenService : IJwtTokenService
{
    private readonly ILogger<JwtTokenService> _logger;
    private readonly string _jwtSecret;

    public JwtTokenService(ILogger<JwtTokenService> logger, IConfiguration config)
    {
        _logger = logger;
        _jwtSecret = config.GetValue<string>("JWT:Secret");
    }

    public string GenerateJwtToken(JwtPayloadDto payload)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSecret));
        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(nameof(JwtPayloadDto.Id).ToLower(), payload.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.GivenName, payload.Fullname),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        var secToken = new JwtSecurityToken(null, null, claims, null,
            null, signingCredentials)
        {
            Payload =
            {
                [nameof(JwtPayloadDto.Permissions).ToLower()] = payload.Permissions,
                [nameof(JwtPayloadDto.Scope).ToLower()] = payload.Scope
            }
        };

        _logger.LogInformation("Creating security token for {@Token}", secToken);

        var jwtToken = tokenHandler.WriteToken(secToken);
        return jwtToken;
    }
}