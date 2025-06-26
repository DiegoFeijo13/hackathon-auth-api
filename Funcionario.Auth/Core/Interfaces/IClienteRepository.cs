using Core.Entities;

namespace Core.Interfaces;

public interface IClienteRepository
{
    Task<ClienteEntity?> GetAsync(string cpf, string senha);
    Task<ClienteEntity> InsertAsync(ClienteEntity cliente);
    Task<bool> DeleteAsync(Guid id);
}