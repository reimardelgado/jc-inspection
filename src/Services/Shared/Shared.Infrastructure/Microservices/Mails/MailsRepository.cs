using System.Net.Http.Headers;
using Newtonsoft.Json;
using Shared.Domain.DTOs.JWT;
using Shared.Domain.DTOs.Mail;
using Shared.Domain.Exceptions;
using Shared.Domain.Interfaces.Microservices.Mails;
using Shared.Domain.Interfaces.Services;
using Shared.Infrastructure.Security;

namespace Shared.Infrastructure.Microservices.Mails;

public class MailsRepository : IMailsRepository
{
    #region Constructor & Properties

    private readonly ILogger<MailsRepository> _logger;
    private readonly IHttpClientFactory _httpClient;
    private readonly IJwtTokenService _jwtTokenService;

    public MailsRepository(ILogger<MailsRepository> logger, IHttpClientFactory httpClient,
        IJwtTokenService jwtTokenService)
    {
        _logger = logger;
        _httpClient = httpClient;
        _jwtTokenService = jwtTokenService;
    }

    #endregion

    #region Public Methods

    public async Task<bool> SendMail(string microserviceName, string? presignedUrl, string template, string receiverId,
        string receiverName, string receiverEmail, string bcc, string subject, string content,
        CancellationToken cancellationToken)
    {
        //Generates Token to authenticate with PointsTransactions service.
        //Microservices don't need permissions to execute actions on controllers.
        var token = _jwtTokenService.GenerateJwtToken(new JwtPayloadDto(Guid.Empty, microserviceName,
            nameof(JwtScope.Microservices), new List<string>()));

        var client = _httpClient.CreateClient("mails");
        client.DefaultRequestHeaders.Clear();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

        try
        {
            var mailRequest = new MailRequest(template, receiverId, receiverName, receiverEmail, bcc, subject, content);
            var requestData =
                new StringContent(JsonConvert.SerializeObject(mailRequest), Encoding.UTF8, "application/json");

            using var httpResponse = await client.PostAsync(presignedUrl, requestData,
                cancellationToken).ConfigureAwait(false);

            if (!httpResponse.IsSuccessStatusCode)
                throw new DomainException("The mail could not be sent");

            await httpResponse.Content.ReadAsStringAsync(cancellationToken);

            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error al enviar la notificacion");
            return false;
        }
    }

    #endregion
}