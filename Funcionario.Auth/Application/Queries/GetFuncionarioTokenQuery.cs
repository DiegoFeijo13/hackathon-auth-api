using Core.Interfaces;
using MediatR;

namespace Application.Queries;

public record GetFuncionarioTokenQuery(string email, string senha) : IRequest<string>;

public class GetFuncionarioTokenQueryHandler(IFuncionarioRepository funcionarioRepository, ITokenService tokenService)
    : IRequestHandler<GetFuncionarioTokenQuery, string>
{
    public async Task<string> Handle(GetFuncionarioTokenQuery request, CancellationToken cancellationToken)
    {
        var user = await funcionarioRepository.GetAsync(request.email, request.senha);

        if (user == null) 
            return string.Empty;

        var token = tokenService.GetFuncionarioToken(user);

        if(string.IsNullOrWhiteSpace(token)) 
            return string.Empty;

        return token;
    }
}
