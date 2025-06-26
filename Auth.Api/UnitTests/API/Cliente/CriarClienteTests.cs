using API.Controllers;
using Application.Commands;
using Core.Entities;
using Core.Enums;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace UnitTests.API.Cliente;
public class CriarClienteTests
{
    private readonly Mock<ISender> _senderMock;
    private readonly ClienteController _sut;

    public CriarClienteTests()
    {
        _senderMock = new Mock<ISender>();
        _sut = new ClienteController(_senderMock.Object);
    }

    [Fact]
    public async Task PostAsync_InformadoDadosValidos_DeverRetornarOk()
    {
        var cliente = new ClienteEntity
        {
            Id = Guid.NewGuid(),
            Nome = "nome",
            Email = "email@test.com",
            Senha = "senha",
            Cpf = "00000000000"
        };
        var command = new AddClienteCommand(cliente.Nome, cliente.Email, cliente.Senha, cliente.Cpf);

        _senderMock
            .Setup(m => m.Send(It.IsAny<AddClienteCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(cliente);

        var result = await _sut.PostAsync(command);

        var createdResult = Assert.IsType<CreatedResult>(result);
        Assert.Equal(cliente, createdResult.Value);
    }

    [Fact]
    public async Task PostAsync_InformadoDadosInvalidos_DeveRetornarBadRequest()
    {        
        var command = new AddClienteCommand("", "", "", "");        

        _senderMock
            .Setup(m => m.Send(It.IsAny<AddClienteCommand>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new ValidationException(""));

        var result = await _sut.PostAsync(command);

        Assert.IsType<BadRequestObjectResult>(result);
    }
}
