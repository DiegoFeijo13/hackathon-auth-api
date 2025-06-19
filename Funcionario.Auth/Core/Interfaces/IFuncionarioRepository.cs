using Domain.Entities;

namespace Core.Interfaces;

public interface IFuncionarioRepository
{
    Task<FuncionarioEntity?> GetAsync(string funcionario, string senha);
    Task<FuncionarioEntity> CriarFuncionarioAsync(FuncionarioEntity funcionario);
    Task<bool> DeletarFuncionarioAsync(Guid id);
}