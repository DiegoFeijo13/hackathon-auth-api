using Application.Commands;
using Application.Extensions;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("[controller]")]
[ApiController]
public class FuncionarioController(ISender sender) : ControllerBase
{
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