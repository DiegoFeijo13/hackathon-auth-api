using Application.Events;
using Application.Validators;
using Core.Interfaces;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Commands;

public record CriarFuncionarioCommand(FuncionarioEntity Funcionario) : IRequest<FuncionarioEntity>;

public class CriarFuncionarioCommandHandler(IFuncionarioRepository funcionarioRepository, IPublisher mediator)
    : IRequestHandler<CriarFuncionarioCommand, FuncionarioEntity>
{
    public async Task<FuncionarioEntity> Handle(CriarFuncionarioCommand request, CancellationToken cancellationToken)
    {
        Validate(request);
        
        var funcionario = await funcionarioRepository.CriarFuncionarioAsync(request.Funcionario);
        await mediator.Publish(new FuncionarioCriadoEvent(funcionario.Id), cancellationToken);
        return funcionario;
    }

    private static void Validate(CriarFuncionarioCommand request)
    {
        var validator = new ModelFuncionarioValidator();
        var result = validator.Validate(request);
        if(!result.IsValid)
            throw new ValidationException(result.Errors);
    }
}