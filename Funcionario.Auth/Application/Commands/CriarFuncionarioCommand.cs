using Application.Events;
using Application.Validators;
using Core.Enums;
using Core.Interfaces;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Commands;

public record CriarFuncionarioCommand(string nome, string email, string senha, FuncionarioFuncao funcao) : IRequest<FuncionarioEntity>;

public class CriarFuncionarioCommandHandler(IFuncionarioRepository funcionarioRepository, IPublisher mediator)
    : IRequestHandler<CriarFuncionarioCommand, FuncionarioEntity>
{
    public async Task<FuncionarioEntity> Handle(CriarFuncionarioCommand request, CancellationToken cancellationToken)
    {
        Validate(request);

        var funcionario = new FuncionarioEntity
        {
            Nome = request.nome,
            Email = request.email,
            Senha = request.senha,
            Funcao = request.funcao
        };
        
        var result = await funcionarioRepository.CriarFuncionarioAsync(funcionario);
        await mediator.Publish(new FuncionarioCriadoEvent(result.Id), cancellationToken);
        return result;
    }

    private static void Validate(CriarFuncionarioCommand request)
    {
        var validator = new ModelFuncionarioValidator();
        var result = validator.Validate(request);
        if(!result.IsValid)
            throw new ValidationException(result.Errors);
    }
}