using Application.Commands;
using Domain.Entities;
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
        var result = await sender.Send(funcionario);
        return Created("", result);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletarFuncionario([FromRoute]Guid id)
    {
        var result = await sender.Send(new DeletarFuncionarioCommand(id));
        return Ok(result);
    }
}