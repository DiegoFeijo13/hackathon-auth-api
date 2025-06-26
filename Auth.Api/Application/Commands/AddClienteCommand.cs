using Application.Events;
using Application.Validators;
using Core.Entities;
using Core.Interfaces;
using FluentValidation;
using MediatR;

namespace Application.Commands;

public record AddClienteCommand(string nome, string cpf, string email, string senha) : IRequest<ClienteEntity>;

public class AddClienteCommandHandler(IClienteRepository repository, IPublisher mediator)
    : IRequestHandler<AddClienteCommand, ClienteEntity>
{
    public async Task<ClienteEntity> Handle(AddClienteCommand request, CancellationToken cancellationToken)
    {
        Validate(request);

        var entity = new ClienteEntity
        {
            Nome = request.nome,
            Cpf = request.cpf,
            Email = request.email,
            Senha = request.senha,            
        };
        
        var result = await repository.InsertAsync(entity);
        await mediator.Publish(new ClienteCriadoEvent(result.Id), cancellationToken);
        return result;
    }

    private static void Validate(AddClienteCommand request)
    {
        var validator = new ModelClienteValidator();
        var result = validator.Validate(request);
        if(!result.IsValid)
            throw new ValidationException(result.Errors);
    }
}