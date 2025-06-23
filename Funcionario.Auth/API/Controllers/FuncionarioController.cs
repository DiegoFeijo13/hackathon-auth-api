using Application.Commands;
using Application.Extensions;
using Core.Enums;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("[controller]")]
[ApiController]
public class FuncionarioController(ISender sender) : ControllerBase
{

    /// <summary>
    /// Adiciona um Funcion�rio na base de dados 
    /// </summary>
    /// <remarks>
    /// Exemplo:
    /// 
    ///  {
    ///     "nome": "batman",
    ///     "email": "batman@gotham.com"
    ///     "senha": "P4ssw0rd",
    ///     "funcao": "0",    
    /// }
    /// </remarks>
    /// <param name="command">Comando com os dados do Funcion�rio</param>
    /// <returns>O Id do Funcion�rio adicionado</returns>
    /// <response code="201">Funcion�rio adicionado na base de dados</response>
    /// <response code="400">Falha ao processar a requisi��o</response>
    /// <response code="500">Erro inesperado</response>
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [HttpPost]
    public async Task<IActionResult> CriarFuncionarioAsync([FromBody] CriarFuncionarioCommand funcionario)
    {
        try
        {
            var result = await sender.Send(funcionario);
            return Created("", result);
        }
        catch(ValidationException ex)
        {
            return BadRequest(ex.ToResultMessage());
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Error = "Ocorreu um erro inesperado.", Details = ex.Message });
        }
        
    }

    /// <summary>
    /// Remove o Funcion�rio na base de dados com o ID informado
    /// </summary>
    /// <param name="id">O ID do Funcion�rio a ser removido</param>
    /// <returns>Resultado da opera��o de remo��o</returns>
    /// <response code="200">Funcion�rio removido com sucesso</response>
    /// <response code="401">Funcion�rio n�o autenticado</response>
    /// <response code="403">Funcion�rio n�o autorizado</response>
    /// <response code="404">Funcion�rio n�o encontrado</response>
    /// <response code="500">Erro inesperado</response>
    [ProducesResponseType(200)]
    [ProducesResponseType(401)]
    [ProducesResponseType(403)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    [Authorize(Roles = FuncionarioPermissao.Gerente)]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletarFuncionarioAsync([FromRoute]Guid id)
    {
        try
        {
            var result = await sender.Send(new DeletarFuncionarioCommand(id));
            return Ok($"Usu�rio com id {id} removido com sucesso.");
        }
        catch(KeyNotFoundException ex)
        {
            return NotFound($"Usu�rio com id {id} n�o encontrado.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Error = "Ocorreu um erro inesperado.", Details = ex.Message });
        }
    }
}