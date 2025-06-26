using Core.Interfaces;
using MediatR;

namespace Application.Queries;

public record GetClienteTokenQuery(string email, string senha) : IRequest<string>;

public class GetClienteTokenQueryHandler(IClienteRepository repository, ITokenService tokenService)
    : IRequestHandler<GetClienteTokenQuery, string>
{
    public async Task<string> Handle(GetClienteTokenQuery request, CancellationToken cancellationToken)
    {
        var user = await repository.GetAsync(request.email, request.senha);

        if (user == null) 
            return string.Empty;

        var token = tokenService.GetClienteToken(user);

        if(string.IsNullOrWhiteSpace(token)) 
            return string.Empty;

        return token;
    }
}
