namespace Auth.API.Model;

public class Funcionario
{
    public Guid Id { get; set; }
    public required string Nome { get; set; }
    public required string Email { get; set; }
    public required string Senha { get; set; }
    public FuncionarioFuncao Funcao { get; set; }
}