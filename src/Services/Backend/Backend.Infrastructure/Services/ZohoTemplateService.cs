using System.Net.Http.Headers;
using System.Text;
using Backend.Application.Configurations;
using Backend.Application.Queries.ZohoQueries;
using Backend.Domain.DTOs.Requests.Zoho;
using Backend.Domain.DTOs.Responses.Zoho;
using Backend.Domain.Interfaces.Services;
using Backend.Domain.MessageHandlers;
using Backend.Domain.SeedWork;
using MediatR;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Shared.Domain.Models;

namespace Backend.Infrastructure.Services;

public class ZohoTemplateService : IZohoTemplateService
{
    private readonly ZohoCrmSettings _zohoCrmSettings;
    private readonly HttpClient _httpClient;
    private readonly IMediator _mediator;

    public ZohoTemplateService(IOptions<ZohoCrmSettings> zohoCrmSettings, HttpClient httpClient, IMediator mediator)
    {
        _zohoCrmSettings = zohoCrmSettings.Value;
        _httpClient = httpClient;
        _mediator = mediator;
    }

    public async Task<string> CreateInZoho(ZohoTemplate entity, CancellationToken cancellationToken)
    {
        var token = await _mediator.Send(new ReadZohoTokenQuery());
        var urlBase = $"{_zohoCrmSettings.ZohoUrlApi}/{_zohoCrmSettings.ModuleSettings.TemplateTc}" ;
        var url = urlBase;
        
        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Zoho-oauthtoken", token.Value!.AccessToken);
        var data = new ZohoTemplateData();
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

    public async Task<string> UpdateInZoho(string idTemplateZoho, string templateId, CancellationToken cancellationToken)
    {
        var zohoTemplate = await ReadTemplateZoho(idTemplateZoho, templateId, cancellationToken);
        if (zohoTemplate is null)
        {
            return EntityResponse<string>.Error(MessageHandler.ZohoInspectionErrorUpdate)!;
        }
        
        var token = await _mediator.Send(new ReadZohoTokenQuery());
        var urlBase = $"{_zohoCrmSettings.ZohoUrlApi}/{_zohoCrmSettings.ModuleSettings.TemplateTc}" ;
        var url = $"{urlBase}/{idTemplateZoho}";
        
        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Zoho-oauthtoken", token.Value!.AccessToken);
        var data = new ZohoTemplateData();
        zohoTemplate.StatusTemplate = StatusEnum.Inactive;
        zohoTemplate.NameTemplate = $"{zohoTemplate.NameTemplate} - {DateTime.Now.ToString("u")}";
        data.Data.Add(zohoTemplate);
        var jsonBody = JsonConvert.SerializeObject(data);
        var request = new StringContent(jsonBody, Encoding.UTF8, "application/json");
        
        using var httpResponse = await _httpClient.PutAsync(url, request, cancellationToken).ConfigureAwait(false);

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
    
    public async Task<ZohoTemplate> ReadTemplateZoho(string idZoho, string templateId, CancellationToken cancellationToken)
    {
        try
        {
            var token = await _mediator.Send(new ReadZohoTokenQuery());
            var urlBase = $"{_zohoCrmSettings.ZohoUrlApi}/{_zohoCrmSettings.ModuleSettings.TemplateTc}" ;
            var url = $"{urlBase}/{idZoho}";
        
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Zoho-oauthtoken", token.Value!.AccessToken);
            
            using var httpResponse = await _httpClient.GetAsync(url, cancellationToken).ConfigureAwait(false);

            if (!httpResponse.IsSuccessStatusCode)
                return EntityResponse<ZohoTemplate>.Error(MessageHandler.ZohoInspectionErrorCreating)!;

            var rawContent = await httpResponse.Content.ReadAsStringAsync(cancellationToken);
            var response = JsonConvert.DeserializeObject<ZohoReadTemplateResponse>(rawContent);

            if (response!.data.Any() && response!.data.ElementAt(0) != null)
            {
                var elem = response!.data.ElementAt(0);
                var zohoTemplate = new ZohoTemplate(elem.Id_Template, elem.Name, elem.Description, elem.Status);
                zohoTemplate.Id = elem.id;
                
                return zohoTemplate;
            }
            return null;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }
}