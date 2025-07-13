namespace FireAndForgetApi.FireAndForgetBus;

public interface IFireAndForgetBus
{
    Task PublishAsync<T>(T integrationEvent, CancellationToken cancellationToken = default) where T : class, IFireAndForgetEvent;
}
