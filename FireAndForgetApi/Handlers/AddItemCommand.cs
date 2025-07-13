using MediatR;

namespace FireAndForgetApi.Handlers;

public record AddItemCommand(Guid Id) : IRequest;

public class AddItemCommandHandler : IRequestHandler<AddItemCommand>
{
    private readonly IPublisher _publisher;
    private readonly ILogger<AddItemCommandHandler> _logger;

    public AddItemCommandHandler(IPublisher publisher, ILogger<AddItemCommandHandler> logger)
    {
        _publisher = publisher;
        _logger = logger;
    }

    public async Task Handle(AddItemCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling AddItemCommand with Id: {Id}", request.Id);

        await _publisher.Publish(new ItemAddedEvent(request.Id), cancellationToken);

        _logger.LogInformation("Published ItemAddedEvent for Id: {Id}", request.Id);

        return;
    }
}