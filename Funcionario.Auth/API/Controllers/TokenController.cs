using Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("[controller]")]
[ApiController]
public class TokenController(ISender sender) : ControllerBase
{

    /// <summary>
    /// Gera um token de autenticação para o usuário e senha informados
    /// </summary>
    /// <param name="usuario">Nome do Usuário cadastrado</param>
    /// <param name="senha">Senha do Usuário</param>
    /// <returns>O token de autenticação da API</returns>
    /// <response code="200">Token gerado com sucesso</response>
    /// <response code="401">Usuário não autenticado</response>    
    /// <response code="500">Erro inesperado</response>
    [HttpGet]
    public async Task<IActionResult> GetToken(string usuario, string senha)
    {
        var token = await sender.Send(new ListarTokenQuery(usuario, senha));

        if(string.IsNullOrEmpty(token) == false)
            return Ok(token);

        return Unauthorized();
    }
}

