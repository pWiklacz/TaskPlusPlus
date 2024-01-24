using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using Serilog.Context;

namespace TaskPlusPlus.Application.Behaviors;

public class LoggingPipelineBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : IResultBase
{
    private readonly ILogger<LoggingPipelineBehavior<TRequest, TResponse>> _logger;

    public LoggingPipelineBehavior(ILogger<LoggingPipelineBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting request {@RequestName}, {@DateTimeUtc}",
            typeof(TRequest).Name,
            DateTime.UtcNow);

        var result = await next();

        if (result.IsFailed)
        {
            using (LogContext.PushProperty("Error", result.Errors, true))
            {
                _logger.LogError("Request failure {@RequestName}, {@DateTimeUtc}",
                    typeof(TRequest).Name,
                    DateTime.UtcNow);
            }
        }
        else
        {
            _logger.LogInformation("Complete request {@RequestName}, {@DateTimeUtc}",
                typeof(TRequest).Name,
                DateTime.UtcNow);
        }

        return result;
    }
}