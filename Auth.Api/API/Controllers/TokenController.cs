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
    /// <param name="cpf">E-mail</param>
    /// <param name="senha">Senha</param>
    /// <returns>O token de autenticação da API</returns>
    /// <response code="200">Token gerado com sucesso</response>
    /// <response code="401">Funcionário não autenticado</response>    
    /// <response code="500">Erro inesperado</response>
    [HttpGet]
    [Route("getfuncionario")]
    public async Task<IActionResult> GetFuncionarioToken(string cpf, string senha)
    {
        var token = await sender.Send(new GetFuncionarioTokenQuery(cpf, senha));

        if(string.IsNullOrEmpty(token) == false)
            return Ok(token);

        return Unauthorized();
    }

    /// <summary>
    /// Gera um token de autenticação para o CPF e senha informados. 
    /// </summary>
    /// <param name="cpf">CPF. Informe somente números.</param>
    /// <param name="senha">Senha</param>
    /// <returns>O token de autenticação da API</returns>
    /// <response code="200">Token gerado com sucesso</response>
    /// <response code="401">Funcionário não autenticado</response>    
    /// <response code="500">Erro inesperado</response>
    [HttpGet]
    [Route("getcliente")]
    public async Task<IActionResult> GetClienteToken(string cpf, string senha)
    {
        var token = await sender.Send(new GetClienteTokenQuery(cpf, senha));

        if (string.IsNullOrEmpty(token) == false)
            return Ok(token);

        return Unauthorized();
    }
}

