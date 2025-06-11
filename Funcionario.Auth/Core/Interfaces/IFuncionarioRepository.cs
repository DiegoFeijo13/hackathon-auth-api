using Domain.Entities;

namespace Core.Interfaces;

public interface IFuncionarioRepository
{
    Task<FuncionarioEntity> CriarFuncionarioAsync(FuncionarioEntity funcionario);
    Task<bool> DeletarFuncionarioAsync(Guid id);
}