using Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("[controller]")]
[ApiController]
public class TokenController(ISender sender) : ControllerBase
{

    /// <summary>
    /// Gera um token de autenticação para o e-mail e senha informados
    /// </summary>
    /// <param name="email">E-mail</param>
    /// <param name="senha">Senha</param>
    /// <returns>O token de autenticação da API</returns>
    /// <response code="200">Token gerado com sucesso</response>
    /// <response code="401">Funcionário não autenticado</response>    
    /// <response code="500">Erro inesperado</response>
    [HttpGet]
    public async Task<IActionResult> GetToken(string email, string senha)
    {
        var token = await sender.Send(new ListarTokenQuery(email, senha));

        if(string.IsNullOrEmpty(token) == false)
            return Ok(token);

        return Unauthorized();
    }
}

