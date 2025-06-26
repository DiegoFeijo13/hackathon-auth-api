using Application.Commands;
using Core.Interfaces;
using Moq;

namespace UnitTests.Application.Funcionario;
public class DeleteFuncionarioCommandHandlerTests
{
    private readonly Mock<IFuncionarioRepository> _repositoryMock;    
    private readonly DeleteFuncionarioCommandHandler _sut;

    public DeleteFuncionarioCommandHandlerTests()
    {
        _repositoryMock = new Mock<IFuncionarioRepository>();        
        _sut = new DeleteFuncionarioCommandHandler(_repositoryMock.Object);
    }

    [Fact]
    public async Task Handle_InformadoUsuarioExistente_DeveRemoverUsuario()
    {
        var id = new Guid();

        _repositoryMock
            .Setup(x => x.DeletarFuncionarioAsync(It.IsAny<Guid>()))
            .ReturnsAsync(true);

        var result = await _sut.Handle(new DeleteFuncionarioCommand(id), CancellationToken.None);

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

        var result = await _sut.Handle(new DeleteFuncionarioCommand(id), CancellationToken.None);

        _repositoryMock.Verify(x => x.DeletarFuncionarioAsync(It.IsAny<Guid>()), Times.Once);

        Assert.False(result);
    }
}
