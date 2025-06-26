using API.Controllers;
using Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace UnitTests.API.Cliente;
public class DeletarClienteTests
{
    private readonly Mock<ISender> _senderMock;
    private readonly ClienteController _sut;

    public DeletarClienteTests()
    {
        _senderMock = new Mock<ISender>();
        _sut = new ClienteController(_senderMock.Object);
    }

    [Fact]
    public async Task DeleteAsync_InformadoIdValido_DeverRetornarOk()
    {
        var id = Guid.NewGuid();

        _senderMock
            .Setup(m => m.Send(It.IsAny<DeleteClienteCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        var result = await _sut.DeleteAsync(id);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal($"Cliente com id {id} removido com sucesso.", okResult.Value);
    }

    [Fact]
    public async Task DeleteAsync_InformadoIdInvalido_DeveRetornarNotFound()
    {
        var id = Guid.NewGuid();

        _senderMock
            .Setup(m => m.Send(It.IsAny<DeleteClienteCommand>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new KeyNotFoundException());

        var result = await _sut.DeleteAsync(id);

        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        Assert.Equal($"Cliente com id {id} não encontrado.", notFoundResult.Value);
    }
}
