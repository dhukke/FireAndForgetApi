using MediatR;

namespace FireAndForgetApi.FireAndForgetBus;

public interface IFireAndForgetEvent : INotification
{
    Guid Id { get; init; }
}
