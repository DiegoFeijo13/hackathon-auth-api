using Application.Commands;
using FluentValidation;

namespace Application.Validators;

public class ModelFuncionarioValidator : AbstractValidator<AddFuncionarioCommand>
{
    public ModelFuncionarioValidator()
    {
        RuleFor(f => f.nome)
            .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
            .MaximumLength(100).WithMessage("O campo {PropertyName} pode ter no máximo 100 caracteres");

        RuleFor(f => f.email)
            .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
            .EmailAddress();
        
        RuleFor(f => f.senha)
            .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
            .MinimumLength(8).WithMessage("O campo {PropertyName} deve ter no mínimo 8 caracteres");

        RuleFor(f => f.funcao)
            .NotNull().WithMessage("O campo {PropertyName} precisa ser fornecido");
    }
}