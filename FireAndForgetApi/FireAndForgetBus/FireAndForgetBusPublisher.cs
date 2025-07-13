using FireAndForgetApi.Events;

namespace FireAndForgetApi.FireAndForgetBus;

public sealed class FireAndForgetBusPublisher(InMemoryMessageQueue queue) : IFireAndForgetBus
{
    async Task IFireAndForgetBus.PublishAsync<T>(T integrationEvent, CancellationToken cancellationToken)
    {
        await queue.Writer.WriteAsync(integrationEvent, cancellationToken);
    }
}
