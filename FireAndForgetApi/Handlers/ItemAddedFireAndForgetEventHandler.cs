using MediatR;

namespace FireAndForgetApi.Handlers;

internal sealed class ItemAddedFireAndForgetEventHandler : INotificationHandler<ItemAddedFireAndForgetEvent>
{
    private readonly ISender _sender;

    public ItemAddedFireAndForgetEventHandler(ISender sender) => _sender = sender;

    public async Task Handle(ItemAddedFireAndForgetEvent notification, CancellationToken cancellationToken)
    {
        await Task.Delay(5000, cancellationToken);

        await _sender.Send(new FinalStageCommand(notification.Id), cancellationToken);

        return;
    }
}