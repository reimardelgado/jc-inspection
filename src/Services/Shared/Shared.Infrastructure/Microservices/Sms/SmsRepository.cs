using System.Net.Http.Headers;
using Newtonsoft.Json;
using Shared.Domain.DTOs.JWT;
using Shared.Domain.DTOs.Sms;
using Shared.Domain.Interfaces.Microservices.Files;
using Shared.Domain.Interfaces.Services;
using Shared.Infrastructure.Security;

namespace Shared.Infrastructure.Microservices.Sms;

public class SmsRepository : ISmsRepository
{
    #region Constructor & Properties

    private readonly ILogger<SmsRepository> _logger;
    private readonly IHttpClientFactory _httpClient;
    private readonly IJwtTokenService _jwtTokenService;

    public SmsRepository(ILogger<SmsRepository> logger, IHttpClientFactory httpClient, IJwtTokenService jwtTokenService)
    {
        _logger = logger;
        _httpClient = httpClient;
        _jwtTokenService = jwtTokenService;
    }

    #endregion

    #region Public Methods

    public async Task<bool> SendSms(string? presignedUrl, string phone, string message,
        CancellationToken cancellationToken)
    {
        //Generates Token to authenticate with PointsTransactions service.
        //Microservices don't need permissions to execute actions on controllers.
        var token = _jwtTokenService.GenerateJwtToken(new JwtPayloadDto(Guid.Empty, "Identity Microservice",
            nameof(JwtScope.Microservices), new List<string>()));

        var client = _httpClient.CreateClient("sms");
        client.DefaultRequestHeaders.Clear();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

        var smsRequest = new SmsRequest(phone, message);
        var requestData =
            new StringContent(JsonConvert.SerializeObject(smsRequest), Encoding.UTF8, "application/json");
        try
        {
            var httpResponse = await client.PostAsync(presignedUrl, requestData, cancellationToken);
            httpResponse.EnsureSuccessStatusCode();

            var rawContent = await httpResponse.Content.ReadAsStringAsync(cancellationToken);
            _logger.LogInformation("Sms enviado: {RawContent}", rawContent);

            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error enviando un SMS al microservicio de Notificaciones");
            return false;
        }
    }

    #endregion
}