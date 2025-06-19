using Domain.Entities;

namespace Core.Interfaces;
public interface ITokenService
{
    string GetToken(FuncionarioEntity funcionario);
}
