namespace Shared.Infrastructure.Behaviors;

public class IntegrationTransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    //private readonly IIntegrationEventService _integrationEventService;
    private readonly ILogger<IntegrationTransactionBehavior<TRequest, TResponse>> _logger;

    public IntegrationTransactionBehavior(ILogger<IntegrationTransactionBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
        RequestHandlerDelegate<TResponse> next)
    {
        // Wait until transaction finish
        var response = await next();

        try
        {
            //await _integrationEventService.PublishIntegrationEventAsync();
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Error publishing integration events");
            throw;
        }

        return response;
    }
}