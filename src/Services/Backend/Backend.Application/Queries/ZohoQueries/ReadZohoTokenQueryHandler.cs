using Backend.Application.Commands.ZohoTokenCommands;
using Backend.Application.Configurations;
using Backend.Application.DTOs.Responses.ZohoResponses;
using Backend.Application.Specifications.ZohoTokenSpecs;
using Backend.Domain.DTOs.Responses.Zoho;
using Backend.Domain.Interfaces.Services;
using Microsoft.Extensions.Options;

namespace Backend.Application.Queries.ZohoQueries
{
    public class ReadZohoTokenQueryHandler : IRequestHandler<ReadZohoTokenQuery, EntityResponse<ZohoTokenResponse>>
    {
        private readonly IRepository<ZohoToken> _repository;
        private readonly IZohoAuthService _zohoAuthService;
        private readonly ILogger<ReadZohoTokenQueryHandler> _logger;
        private readonly ZohoAuthSettings _zohoAuthSettings;
        private readonly IMediator _mediator;
        private ZohoToken? _entity;
        private RefreshTokenModel? _refreshTokenModel;

        public ReadZohoTokenQueryHandler(IRepository<ZohoToken> repository, IZohoAuthService zohoAuthService,
            ILogger<ReadZohoTokenQueryHandler> logger, IOptions<ZohoAuthSettings> zohoAuthSettings, IMediator mediator)
        {
            _repository = repository;
            _zohoAuthService = zohoAuthService;
            _logger = logger;
            _mediator = mediator;
            _zohoAuthSettings = zohoAuthSettings.Value;
        }

        public async Task<EntityResponse<ZohoTokenResponse>> Handle(ReadZohoTokenQuery request,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Init validation token");
            var token = await VerifyTokenAsync(cancellationToken);
            _logger.LogInformation("End validation token");

            if (token is null || token.Expire)
            {
                await GetRefreshTokenModel(cancellationToken);
                
                _entity = new ZohoToken(_refreshTokenModel!.AccessToken);
                var save = await _repository.AddAsync(_entity, cancellationToken);
                await _repository.SaveChangesAsync(cancellationToken);
                
                return EntityResponse.Success(new ZohoTokenResponse(_refreshTokenModel.AccessToken));
            }
            else
            {
                return EntityResponse.Success(new ZohoTokenResponse(token.AccessToken));
            }
        }

        private async Task GetRefreshTokenModel(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Init get new token");
            var refreshToken = _zohoAuthSettings.RefreshToken;
            var clientId = _zohoAuthSettings.ClientId;
            var clientSecret = _zohoAuthSettings.ClientSecret;
            _refreshTokenModel = await _zohoAuthService.GetAccessToken(refreshToken!, clientId!, clientSecret!,cancellationToken);
            _logger.LogInformation("End get new token");
        }
        
        private async Task<TokenModel?> VerifyTokenAsync(CancellationToken cancellationToken)
        {
            var result = new TokenModel();
            var zohoTokenSpec = new ZohoTokenSpec();
            var token = await _repository.GetBySpecAsync(zohoTokenSpec, cancellationToken);
            if(token != null)
            {
                var time = _zohoAuthSettings.Time;
                var createDate = token.CreatedAt;
                var nowDate = DateTime.UtcNow;
                TimeSpan hours = nowDate.Subtract(createDate);
                if (hours.TotalMinutes > Convert.ToDouble(time))
                {
                    result.AccessToken = token.AccessToken;
                    result.Expire = true;
                    
                    // var zohoTokenUpdateCommand =
                    //     new UpdateZohoTokenCommand(token.Id, token.AccessToken, StatusEnum.Deleted);
                    var zohoTokenDeleteCommand =
                        new DeleteZohoTokenCommand(token.Id);
                    await _mediator.Send(zohoTokenDeleteCommand, cancellationToken);
                    
                    return result;
                }
                result.AccessToken = token.AccessToken;
                result.Expire = false;
                return result;
            }

            return null;
        }
    }
}