using System.ComponentModel.DataAnnotations;

namespace Core.Entities;

public class ClienteEntity : BaseEntity
{
    public string Nome { get; set; } = string.Empty;
    public string Cpf { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Senha { get; set; } = string.Empty; 
}