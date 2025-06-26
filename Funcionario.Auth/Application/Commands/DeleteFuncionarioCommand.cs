using Core.Interfaces;
using MediatR;

namespace Application.Commands;

public record DeleteFuncionarioCommand(Guid Id) : IRequest<bool>;

public class DeleteFuncionarioCommandHandler(IFuncionarioRepository funcionarioRepository)
    : IRequestHandler<DeleteFuncionarioCommand, bool>
{
    public async Task<bool> Handle(DeleteFuncionarioCommand request, CancellationToken cancellationToken)
    {
        return await funcionarioRepository.DeletarFuncionarioAsync(request.Id);
    }
}