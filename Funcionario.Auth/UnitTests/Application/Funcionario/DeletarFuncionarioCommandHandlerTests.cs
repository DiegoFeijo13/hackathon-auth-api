using Application.Commands;
using Core.Interfaces;
using Moq;

namespace UnitTests.Application.Funcionario;
public class DeletarFuncionarioCommandHandlerTests
{
    private readonly Mock<IFuncionarioRepository> _repositoryMock;    
    private readonly DeletarFuncionarioCommandHandler _sut;

    public DeletarFuncionarioCommandHandlerTests()
    {
        _repositoryMock = new Mock<IFuncionarioRepository>();        
        _sut = new DeletarFuncionarioCommandHandler(_repositoryMock.Object);
    }

    [Fact]
    public async Task Handle_InformadoUsuarioExistente_DeveRemoverUsuario()
    {
        var id = new Guid();

        _repositoryMock
            .Setup(x => x.DeletarFuncionarioAsync(It.IsAny<Guid>()))
            .ReturnsAsync(true);

        var result = await _sut.Handle(new DeletarFuncionarioCommand(id), CancellationToken.None);

        _repositoryMock.Verify(x => x.DeletarFuncionarioAsync(It.IsAny<Guid>()), Times.Once);
        
        Assert.True(result);
    }

    [Fact]
    public async Task Handle_InformadoUsuarioInexistente_ReturnFalse()
    {
        var id = new Guid();

        _repositoryMock
            .Setup(x => x.DeletarFuncionarioAsync(It.IsAny<Guid>()))
            .ReturnsAsync(false);

        var result = await _sut.Handle(new DeletarFuncionarioCommand(id), CancellationToken.None);

        _repositoryMock.Verify(x => x.DeletarFuncionarioAsync(It.IsAny<Guid>()), Times.Once);

        Assert.False(result);
    }
}
