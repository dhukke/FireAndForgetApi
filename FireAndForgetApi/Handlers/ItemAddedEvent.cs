
using MediatR;

namespace FireAndForgetApi.Handlers;

internal record ItemAddedEvent(Guid Id) : INotification;