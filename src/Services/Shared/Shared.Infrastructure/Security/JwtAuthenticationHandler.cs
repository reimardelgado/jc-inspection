namespace Shared.Infrastructure.Security;

public class JwtAuthenticationSchemaOptions : AuthenticationSchemeOptions
{
}

public class JwtAuthenticationHandler : AuthenticationHandler<JwtAuthenticationSchemaOptions>
{
    #region Constructor & Properties

    private readonly IOptions<JwtAuthenticationSettings> _jwtAuthSettings;
    private readonly ILogger<JwtAuthenticationHandler> _logger;

    public JwtAuthenticationHandler(IOptionsMonitor<JwtAuthenticationSchemaOptions> options,
        ILoggerFactory loggerFactory, UrlEncoder encoder, ISystemClock clock,
        IOptions<JwtAuthenticationSettings> jwtAuthSettings) : base(options, loggerFactory, encoder, clock)
    {
        _logger = loggerFactory.CreateLogger<JwtAuthenticationHandler>();
        _jwtAuthSettings = jwtAuthSettings;
    }

    #endregion

    #region Public Methods

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.ContainsKey("Authorization")) return AuthenticateResult.Fail("Unauthorized");

        string authorizationHeader = Request.Headers["Authorization"];

        var authenticateResult = ValidateAuthorizationHeader(authorizationHeader);

        return await Task.FromResult(authenticateResult ?? ValidateAuthenticationToken(authorizationHeader));
    }

    #endregion

    #region Private Methods

    private AuthenticateResult? ValidateAuthorizationHeader(string authorizationHeader)
    {
        if (string.IsNullOrEmpty(authorizationHeader)) return AuthenticateResult.NoResult();

        if (!authorizationHeader.StartsWith("bearer", StringComparison.OrdinalIgnoreCase))
            return AuthenticateResult.Fail("Unauthorized");

        var token = authorizationHeader["bearer".Length..].Trim();

        return string.IsNullOrEmpty(token)
            ? AuthenticateResult.Fail("Unauthorized")
            : null; // Return null if validation is True
    }

    private AuthenticateResult ValidateAuthenticationToken(string authorizationHeader)
    {
        try
        {
            var token = authorizationHeader["bearer".Length..].Trim();
            return HandleToken(token, _jwtAuthSettings.Value.Secret);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "There was an error handling jwt token");
            return AuthenticateResult.Fail(ex.Message);
        }
    }

    private AuthenticateResult HandleToken(string token, string? secret)
    {
        var validatedPrincipal = ValidateToken(token, secret);

        var principal = new GenericPrincipal(validatedPrincipal.Identity!, null); //NOSONAR
        var ticket = new AuthenticationTicket(principal, Scheme.Name);
        return AuthenticateResult.Success(ticket);
    }

    private ClaimsPrincipal ValidateToken(string authToken, string? secret)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var validationParameters = new TokenValidationParameters
        {
            ValidateLifetime = false, // Because there is no expiration in the generated token
            ValidateAudience = false, // Because there is no audiance in the generated token
            ValidateIssuer = false, // Because there is no issuer in the generated token,
            // The same key as the one that generate the token
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret))
        };

        return tokenHandler.ValidateToken(authToken, validationParameters, out _);
    }

    #endregion
}