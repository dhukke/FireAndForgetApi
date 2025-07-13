
using FireAndForgetApi.Events;
using FireAndForgetApi.FireAndForgetBus;

namespace FireAndForgetApi;

public static class FireAndForgetDependency
{
    public static void AddFireAndForget(this IServiceCollection services)
    {
        services.AddSingleton<InMemoryMessageQueue>();
        services.AddSingleton<IFireAndForgetBus, FireAndForgetBusPublisher>();
        services.AddHostedService<FireAndForgetEventProcessorJob>();
    }
}
