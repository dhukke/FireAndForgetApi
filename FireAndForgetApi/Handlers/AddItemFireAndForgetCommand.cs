using FireAndForgetApi.FireAndForgetBus;
using MediatR;

namespace FireAndForgetApi.Handlers;

public record AddItemFireAndForgetCommand(Guid Id) : IRequest;

public class AddItemFireAndForgetCommandHandler : IRequestHandler<AddItemFireAndForgetCommand>
{
    private readonly IFireAndForgetBus _publisher;
    private readonly ILogger<AddItemFireAndForgetCommandHandler> _logger;

    public AddItemFireAndForgetCommandHandler(IFireAndForgetBus publisher, ILogger<AddItemFireAndForgetCommandHandler> logger)
    {
        _publisher = publisher;
        _logger = logger;
    }

    public async Task Handle(AddItemFireAndForgetCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling AddItemCommandA with Id: {Id}", request.Id);

        await _publisher.PublishAsync(new ItemAddedFireAndForgetEvent(request.Id), cancellationToken);

        _logger.LogInformation("Published ItemAddedEventA for Id: {Id}", request.Id);

        return;
    }
}