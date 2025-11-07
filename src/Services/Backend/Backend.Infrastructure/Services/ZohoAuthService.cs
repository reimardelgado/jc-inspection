using Backend.Application.Configurations;
using Backend.Domain.DTOs.Responses.Zoho;
using Backend.Domain.Interfaces.Services;
using Backend.Domain.MessageHandlers;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;
using Shared.Domain.Models;

namespace Backend.Infrastructure.Services;

public class ZohoAuthService : IZohoAuthService
{
    private readonly ZohoAuthSettings _zohoAuthSettings;
    private readonly HttpClient _httpClient;

    public ZohoAuthService(IOptions<ZohoAuthSettings> zohoAuthSettings, HttpClient httpClient)
    {
        _httpClient = httpClient;
        _zohoAuthSettings = zohoAuthSettings.Value;
    }

    public async Task<RefreshTokenModel> GetAccessToken(string refreshToken, string clientId, string clientSecret, CancellationToken cancellationToken)
    {
        var urlBase = _zohoAuthSettings.Url;
        var url = string.Format("{0}?refresh_token={1}&client_id={2}&client_secret={3}&grant_type=refresh_token", urlBase, refreshToken, clientId, clientSecret);
        using var httpResponse = await _httpClient.PostAsync(url, null,
            cancellationToken).ConfigureAwait(false);

        if (!httpResponse.IsSuccessStatusCode)
            return EntityResponse<RefreshTokenModel>.Error(MessageHandler.ZohoErrorGeneratedToken)!;

        var rawContent = await httpResponse.Content.ReadAsStringAsync(cancellationToken);
        var response = JsonConvert.DeserializeObject<RefreshTokenModel>(rawContent);

        return response!;
    }
}