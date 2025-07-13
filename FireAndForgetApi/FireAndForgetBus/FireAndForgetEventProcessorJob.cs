
using FireAndForgetApi.Events;
using MediatR;
using Polly;
using Polly.Retry;

namespace FireAndForgetApi.FireAndForgetBus;

public sealed class FireAndForgetEventProcessorJob(
    InMemoryMessageQueue queue,
    IPublisher publisher,
    ILogger<FireAndForgetEventProcessorJob> logger
) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // Define a retry policy: retry 3 times with exponential backoff
        AsyncRetryPolicy retryPolicy = Policy
            .Handle<Exception>()
            .WaitAndRetryAsync(
                retryCount: 3,
                sleepDurationProvider: attempt => TimeSpan.FromSeconds(Math.Pow(2, attempt)),
                onRetry: (exception, timeSpan, retryCount, context) =>
                {
                    logger.LogWarning(exception, "Retry {RetryCount} after {Delay} seconds due to error processing fire and forget event.", retryCount, timeSpan.TotalSeconds);
                });

        await foreach (IFireAndForgetEvent fireAndForgetEvent in queue.Reader.ReadAllAsync(stoppingToken))
        {
            try
            {
                logger.LogInformation("Processing fire and forget event: {@FireAndForgetEvent}", fireAndForgetEvent);

                await retryPolicy.ExecuteAsync(async () =>
                {
                    await publisher.Publish(fireAndForgetEvent, stoppingToken);
                });

                logger.LogInformation("Successfully processed fire and forget event: {@FireAndForgetEvent}", fireAndForgetEvent);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error processing fire and forget event after retries: {@FireAndForgetEvent}", fireAndForgetEvent);
            }
        }
    }
}
