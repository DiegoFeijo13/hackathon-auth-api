using Core.Interfaces;
using MediatR;

namespace Application.Queries;

public record ListarTokenQuery(string email, string senha) : IRequest<string>;

public class ListarTokenQueryHandler(IFuncionarioRepository funcionarioRepository, ITokenService tokenService)
    : IRequestHandler<ListarTokenQuery, string>
{
    public async Task<string> Handle(ListarTokenQuery request, CancellationToken cancellationToken)
    {
        var user = await funcionarioRepository.GetAsync(request.email, request.senha);

        if (user == null) 
            return string.Empty;

        var token = tokenService.GetToken(user);

        if(string.IsNullOrWhiteSpace(token)) 
            return string.Empty;

        return token;
    }
}
