using API.Controllers;
using Application.Commands;
using Core.Enums;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace UnitTests.API.Funcionario;
public class CriarFuncionarioTests
{
    private readonly Mock<ISender> _senderMock;
    private readonly FuncionarioController _sut;

    public CriarFuncionarioTests()
    {
        _senderMock = new Mock<ISender>();
        _sut = new FuncionarioController(_senderMock.Object);
    }

    [Fact]
    public async Task CriarFuncionarioAsync_InformadoDadosValidos_DeverRetornarOk()
    {
        var funcionario = new FuncionarioEntity
        {
            Id = Guid.NewGuid(),
            Nome = "nome",
            Email = "email@test.com",
            Senha = "senha",
            Funcao = FuncionarioFuncao.Atendente
        };
        var command = new CriarFuncionarioCommand(funcionario.Nome, funcionario.Email, funcionario.Senha, funcionario.Funcao);

        _senderMock
            .Setup(m => m.Send(It.IsAny<CriarFuncionarioCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(funcionario);

        var result = await _sut.CriarFuncionarioAsync(command);

        var createdResult = Assert.IsType<CreatedResult>(result);
        Assert.Equal(funcionario, createdResult.Value);
    }

    [Fact]
    public async Task CriarFuncionarioAsync_InformadoDadosInvalidos_DeveRetornarBadRequest()
    {        
        var command = new CriarFuncionarioCommand("", "", "", FuncionarioFuncao.Atendente);        

        _senderMock
            .Setup(m => m.Send(It.IsAny<CriarFuncionarioCommand>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new ValidationException(""));

        var result = await _sut.CriarFuncionarioAsync(command);

        Assert.IsType<BadRequestObjectResult>(result);
    }
}
