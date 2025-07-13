namespace FireAndForgetApi.FireAndForgetBus;

public abstract record FireAndForgetEvent(Guid Id) : IFireAndForgetEvent;