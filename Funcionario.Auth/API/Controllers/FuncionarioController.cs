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
    /// Adiciona um Funcionário na base de dados 
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
    /// <param name="command">Comando com os dados do Funcionário</param>
    /// <returns>O Id do Funcionário adicionado</returns>
    /// <response code="201">Funcionário adicionado na base de dados</response>
    /// <response code="400">Falha ao processar a requisição</response>
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
    /// Remove o Funcionário na base de dados com o ID informado
    /// </summary>
    /// <param name="id">O ID do Funcionário a ser removido</param>
    /// <returns>Resultado da operação de remoção</returns>
    /// <response code="200">Funcionário removido com sucesso</response>
    /// <response code="401">Funcionário não autenticado</response>
    /// <response code="403">Funcionário não autorizado</response>
    /// <response code="404">Funcionário não encontrado</response>
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
            return Ok($"Usuário com id {id} removido com sucesso.");
        }
        catch(KeyNotFoundException ex)
        {
            return NotFound($"Usuário com id {id} não encontrado.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Error = "Ocorreu um erro inesperado.", Details = ex.Message });
        }
    }
}