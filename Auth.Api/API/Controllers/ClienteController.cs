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
public class ClienteController(ISender sender) : ControllerBase
{

    /// <summary>
    /// Adiciona um Cliente na base de dados 
    /// </summary>
    /// <remarks>
    /// Exemplo:
    /// 
    ///  {
    ///     "nome": "Alfred",
    ///     "cpf": "11111111111"
    ///     "email": "alfred@gotham.com"
    ///     "senha": "P4ssw0rd",    
    /// }
    /// </remarks>
    /// <param name="command">Comando com os dados do Cliente</param>
    /// <returns>O Id do Cliente adicionado</returns>
    /// <response code="201">Cliente adicionado na base de dados</response>
    /// <response code="400">Falha ao processar a requisição</response>
    /// <response code="500">Erro inesperado</response>
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] AddClienteCommand command)
    {
        try
        {
            var result = await sender.Send(command);
            return Created("", result.Id);
        }
        catch (ValidationException ex)
        {
            return BadRequest(ex.ToResultMessage());
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Error = "Ocorreu um erro inesperado.", Details = ex.Message });
        }

    }

    /// <summary>
    /// Remove o Cliente na base de dados com o ID informado
    /// </summary>
    /// <param name="id">O ID do Cliente a ser removido</param>
    /// <returns>Resultado da operação de remoção</returns>
    /// <response code="200">Cliente removido com sucesso</response>
    /// <response code="401">Usuário não autenticado</response>
    /// <response code="403">Usuário não autorizado</response>
    /// <response code="404">Cliente não encontrado</response>
    /// <response code="500">Erro inesperado</response>
    [ProducesResponseType(200)]
    [ProducesResponseType(401)]
    [ProducesResponseType(403)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    [Authorize(Roles = FuncionarioPermissao.Gerente)]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
    {
        try
        {
            var result = await sender.Send(new DeleteClienteCommand(id));
            return Ok($"Cliente com id {id} removido com sucesso.");
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound($"Cliente com id {id} não encontrado.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Error = "Ocorreu um erro inesperado.", Details = ex.Message });
        }
    }
}