using Core.Entities;

namespace Core.Interfaces;
public interface ITokenService
{
    string GetFuncionarioToken(FuncionarioEntity funcionario);
    string GetClienteToken(ClienteEntity cliente);
}
