using Core.Interfaces;
using MediatR;

namespace Application.Commands;

public record DeleteClienteCommand(Guid Id) : IRequest<bool>;

public class DeleteClienteCommandHandler(IClienteRepository repository)
    : IRequestHandler<DeleteClienteCommand, bool>
{
    public async Task<bool> Handle(DeleteClienteCommand request, CancellationToken cancellationToken)
    {
        return await repository.DeleteAsync(request.Id);
    }
}