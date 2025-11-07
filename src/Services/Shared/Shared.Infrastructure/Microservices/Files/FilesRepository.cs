using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Shared.Domain.DTOs.File;
using Shared.Domain.DTOs.JWT;
using Shared.Domain.Exceptions;
using Shared.Domain.Interfaces.Microservices.Files;
using Shared.Domain.Interfaces.Services;
using Shared.Domain.Models;
using Shared.Infrastructure.Security;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Shared.Infrastructure.Microservices.Files;

public class FilesRepository : IFilesRepository
{
    #region Constructor & Properties

    private readonly ILogger<FilesRepository> _logger;
    private readonly IHttpClientFactory _httpClient;
    private readonly IConfiguration _configuration;
    private readonly IJwtTokenService _jwtTokenService;

    public FilesRepository(ILogger<FilesRepository> logger, IHttpClientFactory httpClient,
        IConfiguration configuration, IJwtTokenService jwtTokenService)
    {
        _logger = logger;
        _httpClient = httpClient;
        _configuration = configuration;
        _jwtTokenService = jwtTokenService;
    }

    #endregion

    #region Public Methods

    public async Task<Stream> DownloadFileAsync(string presignedUrl, CancellationToken cancellationToken)
    {
        var client = _httpClient.CreateClient("files");
        client.DefaultRequestHeaders.Clear();
        var responseMessage =
            await client.GetAsync(presignedUrl, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
        responseMessage.EnsureSuccessStatusCode();
        var streamResponse = await responseMessage.Content.ReadAsStreamAsync(cancellationToken);
        if (!responseMessage.IsSuccessStatusCode)
        {
            throw new Exception("Failed to connect at AWS3 storage");
        }

        return streamResponse;
    }

    public async Task<EntityResponse<CreateFileResponseDto>> UploadFileAsync(byte[] fileBytes, string fileName,
        string contentType,
        string? nameService)
    {
        //Generates Token to authenticate with PointsTransactions service.
        //Microservices don't need permissions to execute actions on controllers.
        var token = _jwtTokenService.GenerateJwtToken(new JwtPayloadDto(Guid.Empty, nameService ?? string.Empty,
            nameof(JwtScope.Microservices), new List<string>()));

        var client = _httpClient.CreateClient(nameService ?? string.Empty);
        client.DefaultRequestHeaders.Clear();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
        var headersKeyValue = new Dictionary<string, string>();

        var bucketName = _configuration.GetValue<string>("AWS:S3:BucketName");
        var bucketPath = _configuration.GetValue<string>("AWS:S3:BucketPath");
        var bucketAccessType = _configuration.GetValue<string>("AWS:S3:BucketAccessType");

        headersKeyValue.Add("X-Bucket-Name", bucketName);
        headersKeyValue.Add("X-Bucket-Path", bucketPath);
        headersKeyValue.Add("X-Access-Type", bucketAccessType);
        headersKeyValue.Add("X-File-Name", fileName);
        foreach (var (key, value) in headersKeyValue)
        {
            client.DefaultRequestHeaders.Add(key, value);
        }

        var content = new MultipartFormDataContent();
        var baContent = new ByteArrayContent(fileBytes);
        baContent.Headers.ContentType = new MediaTypeHeaderValue(contentType);
        content.Add(baContent, "BulkLoad", fileName);

        var baseUrl = _configuration.GetValue<string>("FileMicroservice:Url");
        var createFileUrl = string.Format(_configuration.GetValue<string>("FileMicroservice:UploadFileUrl"), baseUrl);

        _logger.LogInformation("{CreateFileUrl}", createFileUrl);

        var response = await client.PostAsync(createFileUrl, content);
        try
        {
            response.EnsureSuccessStatusCode();
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Cannot add file into AWS S3");
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            var responseDto = JsonSerializer.Deserialize<CreateFileResponseDto>(responseContent,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return responseDto!;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error uploading a file");
            return EntityResponse<CreateFileResponseDto>.Error("Error al subir el fichero");
        }
    }

    public async Task<PresignedUrlDto> PreSignedUrl(string url, string nameService)
    {
        var baseUrl = _configuration.GetValue<string>("FileMicroservice:Url");
        var uri = string.Format(_configuration.GetValue<string>("FileMicroservice:GeneratePresignedUrl"), baseUrl);

        var token = _jwtTokenService.GenerateJwtToken(new JwtPayloadDto(Guid.Empty, nameService,
            nameof(JwtScope.Microservices), new List<string>()));

        var client = _httpClient.CreateClient("files");
        client.DefaultRequestHeaders.Clear();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

        var content = JsonConvert.SerializeObject(url);
        var httpResponse =
            await client.PostAsync(uri, new StringContent(content, Encoding.Default, "application/json"));
        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception($"Cannot post {uri}");
        }

        var response = await httpResponse.Content.ReadAsStringAsync();
        var createdTask =
            JsonConvert.DeserializeObject<PresignedUrlDto>(response);

        if (createdTask is not null) return createdTask;

        _logger.LogError("No se pudo deserializar el mensaje devuelto por Amazon S3: {@Response}", response);
        throw new DomainException("Se ha producido un error. Por favor contacte al Administrador");
    }

    public async Task<bool> DeleteFile(string url, string nameService)
    {
        var baseUrl = _configuration.GetValue<string>("FileMicroservice:Url");
        var uri = string.Format(_configuration.GetValue<string>("FileMicroservice:DeleteFileUrl"), baseUrl);

        var token = _jwtTokenService.GenerateJwtToken(new JwtPayloadDto(Guid.Empty, nameService,
            nameof(JwtScope.Microservices), new List<string>()));

        var client = _httpClient.CreateClient("files");

        client.DefaultRequestHeaders.Clear();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        var content = new StringContent(JsonConvert.SerializeObject(new { url }), Encoding.UTF8, "application/json");
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Delete,
            RequestUri = new Uri(uri),
            Content = content
        };
        var httpResponse = await client.SendAsync(request);

        return httpResponse.IsSuccessStatusCode;
    }

    #endregion
}