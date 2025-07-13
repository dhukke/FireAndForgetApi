using MediatR;

namespace FireAndForgetApi.Handlers;

internal record FinalStageCommand(Guid Id) : IRequest;

internal class FinalStageCommandHandler : IRequestHandler<FinalStageCommand>
{
    private readonly ILogger<FinalStageCommandHandler> _logger;

    public FinalStageCommandHandler(ILogger<FinalStageCommandHandler> logger)
    {
        _logger = logger;
    }

    public async Task Handle(FinalStageCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling FinalStageCommand with Id: {Id}", request.Id);

        await Task.Delay(1, cancellationToken);
    }
}