using FireAndForgetApi.FireAndForgetBus;

namespace FireAndForgetApi.Handlers;

internal record ItemAddedFireAndForgetEvent(Guid Id) : FireAndForgetEvent(Id);