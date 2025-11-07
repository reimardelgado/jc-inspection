using System.Net.Http.Headers;
using System.Text;
using Backend.Application.Configurations;
using Backend.Application.Queries.ZohoQueries;
using Backend.Domain.DTOs.Requests.Zoho;
using Backend.Domain.DTOs.Responses.Zoho;
using Backend.Domain.Interfaces.Services;
using Backend.Domain.MessageHandlers;
using MediatR;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Shared.Domain.Models;

namespace Backend.Infrastructure.Services;

public class ZohoExternalUserService : IZohoExternalUserService
{
    private readonly ZohoCrmSettings _zohoCrmSettings;
    private readonly HttpClient _httpClient;
    private readonly IMediator _mediator;

    public ZohoExternalUserService(IOptions<ZohoCrmSettings> zohoCrmSettings, HttpClient httpClient, IMediator mediator)
    {
        _zohoCrmSettings = zohoCrmSettings.Value;
        _httpClient = httpClient;
        _mediator = mediator;
    }

    public async Task<string> CreateInZoho(ZohoExternalUser entity, CancellationToken cancellationToken)
    {
        var token = await _mediator.Send(new ReadZohoTokenQuery());
        var urlBase = $"{_zohoCrmSettings.ZohoUrlApi}/{_zohoCrmSettings.ModuleSettings.ExternalUser}" ;
        var url = urlBase;
        
        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Zoho-oauthtoken", token.Value!.AccessToken);
        var data = new ZohoExternalUserData();
        data.Data.Add(entity);
        var jsonBody = JsonConvert.SerializeObject(data);
        var request = new StringContent(jsonBody, Encoding.UTF8, "application/json");
        
        using var httpResponse = await _httpClient.PostAsync(url, request, cancellationToken).ConfigureAwait(false);

        if (!httpResponse.IsSuccessStatusCode)
            return EntityResponse<string>.Error(MessageHandler.ZohoExternalUserErrorCreating)!;

        var rawContent = await httpResponse.Content.ReadAsStringAsync(cancellationToken);
        var response = JsonConvert.DeserializeObject<ZohoCreateRecordResponse>(rawContent);

        if (response!.data.ElementAt(0).code == "SUCCESS")
        {
            return response.data.ElementAt(0).details.id;
        }
        return string.Empty;
    }

    public async Task<string> UpdateInZoho(ZohoExternalUser entity, CancellationToken cancellationToken)
    {
        var token = await _mediator.Send(new ReadZohoTokenQuery());
        var urlBase = $"{_zohoCrmSettings.ZohoUrlApi}/{_zohoCrmSettings.ModuleSettings.TemplateTc}" ;
        var url = urlBase;
        
        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Zoho-oauthtoken", token.Value!.AccessToken);
        var data = new ZohoExternalUserData();
        data.Data.Add(entity);
        var jsonBody = JsonConvert.SerializeObject(data);
        var request = new StringContent(jsonBody, Encoding.UTF8, "application/json");
        
        using var httpResponse = await _httpClient.PostAsync(url, request, cancellationToken).ConfigureAwait(false);

        if (!httpResponse.IsSuccessStatusCode)
            return EntityResponse<string>.Error(MessageHandler.ZohoTemplateErrorCreating)!;

        var rawContent = await httpResponse.Content.ReadAsStringAsync(cancellationToken);
        var response = JsonConvert.DeserializeObject<ZohoCreateRecordResponse>(rawContent);
        if (response!.data.ElementAt(0).code == "SUCCESS")
        {
            return response.data.ElementAt(0).details.id;
        }
        return string.Empty;
    }
}