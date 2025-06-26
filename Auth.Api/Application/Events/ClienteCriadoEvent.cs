using MediatR;

namespace Application.Events;

public record ClienteCriadoEvent(Guid Id) : INotification;