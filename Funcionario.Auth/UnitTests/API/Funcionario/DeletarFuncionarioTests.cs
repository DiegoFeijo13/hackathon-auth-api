using API.Controllers;
using Application.Commands;
using Core.Enums;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace UnitTests.API.Funcionario;
public class DeletarFuncionarioTests
{
    private readonly Mock<ISender> _senderMock;
    private readonly FuncionarioController _sut;

    public DeletarFuncionarioTests()
    {
        _senderMock = new Mock<ISender>();
        _sut = new FuncionarioController(_senderMock.Object);
    }

    [Fact]
    public async Task DeletarFuncionarioAsync_InformadoIdValido_DeverRetornarOk()
    {
        var id = Guid.NewGuid();

        _senderMock
            .Setup(m => m.Send(It.IsAny<DeletarFuncionarioCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        var result = await _sut.DeletarFuncionarioAsync(id);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal($"Usuário com id {id} removido com sucesso.", okResult.Value);
    }

    [Fact]
    public async Task DeletarFuncionarioAsync_InformadoIdInvalido_DeveRetornarNotFound()
    {
        var id = Guid.NewGuid();

        _senderMock
            .Setup(m => m.Send(It.IsAny<DeletarFuncionarioCommand>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new KeyNotFoundException());

        var result = await _sut.DeletarFuncionarioAsync(id);

        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        Assert.Equal($"Usuário com id {id} não encontrado.", notFoundResult.Value);
    }
}
