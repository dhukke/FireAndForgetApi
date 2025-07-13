using MediatR;

namespace FireAndForgetApi.Handlers;

internal sealed class ItemAddedEventHandler : INotificationHandler<ItemAddedEvent>
{
    public Task Handle(ItemAddedEvent notification, CancellationToken cancellationToken) => Task.Delay(5000, cancellationToken);
}