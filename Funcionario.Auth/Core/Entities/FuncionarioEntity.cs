using Core.Enums;

namespace Domain.Entities;

public class FuncionarioEntity
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Senha { get; set; } = string.Empty;
    public FuncionarioFuncao? Funcao { get; set; }
}