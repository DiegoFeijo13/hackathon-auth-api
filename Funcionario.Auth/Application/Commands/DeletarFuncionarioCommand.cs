using Core.Interfaces;
using MediatR;

namespace Application.Commands;

public record DeletarFuncionarioCommand(Guid Id) : IRequest<bool>;

public class RemoverFuncionarioCommandHandler(IFuncionarioRepository funcionarioRepository)
    : IRequestHandler<DeletarFuncionarioCommand, bool>
{
    public async Task<bool> Handle(DeletarFuncionarioCommand request, CancellationToken cancellationToken)
    {
        return await funcionarioRepository.DeletarFuncionarioAsync(request.Id);
    }
}