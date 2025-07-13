using FireAndForgetApi.Events;
using MediatR;

namespace FireAndForgetApi.FireAndForgetBus;

public sealed class FireAndForgetEventProcessorJob(
    InMemoryMessageQueue queue,
    IPublisher publisher,
    ILogger<FireAndForgetEventProcessorJob> logger
) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await foreach (IFireAndForgetEvent fireAndForgetEvent in queue.Reader.ReadAllAsync(stoppingToken))
        {
            try
            {
                logger.LogInformation("Processing fire and forget event: {@FireAndForgetEvent}", fireAndForgetEvent);

                await publisher.Publish(fireAndForgetEvent, stoppingToken);

                logger.LogInformation("Successfully processed fire and forget event: {@FireAndForgetEvent}", fireAndForgetEvent);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error processing fire and forget event: {@FireAndForgetEvent}", fireAndForgetEvent);
            }
        }
    }
}
