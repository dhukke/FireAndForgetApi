using MediatR;

namespace FireAndForgetApi.Handlers;

internal sealed class ItemAddedFireAndForgetEventHandler : INotificationHandler<ItemAddedFireAndForgetEvent>
{
    public Task Handle(ItemAddedFireAndForgetEvent notification, CancellationToken cancellationToken) => Task.Delay(5000, cancellationToken);
}