using Application.Commands;
using Core.Interfaces;
using Moq;

namespace UnitTests.Application.Cliente;
public class DeleteClienteCommandHandlerTests
{
    private readonly Mock<IClienteRepository> _repositoryMock;    
    private readonly DeleteClienteCommandHandler _sut;

    public DeleteClienteCommandHandlerTests()
    {
        _repositoryMock = new Mock<IClienteRepository>();        
        _sut = new DeleteClienteCommandHandler(_repositoryMock.Object);
    }

    [Fact]
    public async Task Handle_InformadoUsuarioExistente_DeveRemoverUsuario()
    {
        var id = new Guid();

        _repositoryMock
            .Setup(x => x.DeleteAsync(It.IsAny<Guid>()))
            .ReturnsAsync(true);

        var result = await _sut.Handle(new DeleteClienteCommand(id), CancellationToken.None);

        _repositoryMock.Verify(x => x.DeleteAsync(It.IsAny<Guid>()), Times.Once);
        
        Assert.True(result);
    }

    [Fact]
    public async Task Handle_InformadoUsuarioInexistente_ReturnFalse()
    {
        var id = new Guid();

        _repositoryMock
            .Setup(x => x.DeleteAsync(It.IsAny<Guid>()))
            .ReturnsAsync(false);

        var result = await _sut.Handle(new DeleteClienteCommand(id), CancellationToken.None);

        _repositoryMock.Verify(x => x.DeleteAsync(It.IsAny<Guid>()), Times.Once);

        Assert.False(result);
    }
}
