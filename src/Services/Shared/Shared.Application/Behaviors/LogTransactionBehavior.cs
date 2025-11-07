using MediatR;
using Microsoft.Extensions.Logging;

namespace Shared.Application.Behaviors;

public class LogTransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<LogTransactionBehavior<TRequest, TResponse>> _logger;

    public LogTransactionBehavior(ILogger<LogTransactionBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
        RequestHandlerDelegate<TResponse> next)
    {
        var response = await next();

        if (response is EntityResponse<TResponse> { IsSuccess: false } entityResponse)
        {
            _logger.LogInformation("{Code}: {Message}", -1, entityResponse.Errors.Aggregate((i, j) => i + ", " + j));
        }

        return response;
    }
}