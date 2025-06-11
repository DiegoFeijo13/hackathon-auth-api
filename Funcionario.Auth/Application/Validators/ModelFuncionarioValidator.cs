using Application.Commands;
using FluentValidation;

namespace Application.Validators;

public class ModelFuncionarioValidator : AbstractValidator<CriarFuncionarioCommand>
{
    public ModelFuncionarioValidator()
    {
        RuleFor(f => f.Funcionario.Nome)
            .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
            .MaximumLength(100).WithMessage("O campo {PropertyName} pode ter no máximo 100 caracteres");

        RuleFor(f => f.Funcionario.Email)
            .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
            .EmailAddress();
        
        RuleFor(f => f.Funcionario.Senha)
            .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
            .MinimumLength(8).WithMessage("O campo {PropertyName} deve ter no mínimo 8 caracteres");

        RuleFor(f => f.Funcionario.Funcao)
            .NotNull().WithMessage("O campo {PropertyName} precisa ser fornecido");
    }
}