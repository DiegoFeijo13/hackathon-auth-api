using MediatR;

namespace Application.Events;

public record FuncionarioCriadoEvent(Guid Id) : INotification;