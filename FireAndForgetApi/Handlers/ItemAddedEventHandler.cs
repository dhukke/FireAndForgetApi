using MediatR;

namespace FireAndForgetApi.Handlers;

internal sealed class ItemAddedEventHandler : INotificationHandler<ItemAddedEvent>
{
    private readonly ISender _sender;

    public ItemAddedEventHandler(ISender sender) => _sender = sender;

    public async Task Handle(ItemAddedEvent notification, CancellationToken cancellationToken)
    {
        await Task.Delay(5000, cancellationToken);

        await _sender.Send(new FinalStageCommand(notification.Id), cancellationToken);

        return;
    }
}