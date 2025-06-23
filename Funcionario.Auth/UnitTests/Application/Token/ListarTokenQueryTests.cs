using Application.Queries;
using Core.Interfaces;
using Domain.Entities;
using Moq;

namespace UnitTests.Application.Token;
public class ListarTokenQueryTests
{
    private readonly Mock<IFuncionarioRepository> _repositoryMock;
    private readonly Mock<ITokenService> _tokenServiceMock;
    private readonly ListarTokenQueryHandler _sut;

    public ListarTokenQueryTests()
    {
        _repositoryMock = new Mock<IFuncionarioRepository>();
        _tokenServiceMock = new Mock<ITokenService>();
        _sut = new ListarTokenQueryHandler(_repositoryMock.Object, _tokenServiceMock.Object);
    }

    [Fact]
    public async Task Handle_InformadoDadosValidos_DeverRetornarToken()
    {
        var query = new ListarTokenQuery("user", "password");
        var expectedToken = "generatedToken";
        

        _repositoryMock.Setup(x => x.GetAsync(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(new FuncionarioEntity());
        _tokenServiceMock.Setup(x => x.GetToken(It.IsAny<FuncionarioEntity>()))
            .Returns(expectedToken);

        var result = await _sut.Handle(query, CancellationToken.None);

        Assert.Equal(expectedToken, result);
    }

    [Fact]
    public async Task Handle_InformadoDadosInvalidos_DeverRetornarStringVazio()
    {
        var query = new ListarTokenQuery("user", "password");
        var expectedToken = string.Empty;

        _repositoryMock.Setup(x => x.GetAsync(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(default(FuncionarioEntity));

        var result = await _sut.Handle(query, CancellationToken.None);

        _tokenServiceMock.Verify(x => x.GetToken(It.IsAny<FuncionarioEntity>()), Times.Never);

        Assert.Equal(expectedToken, result);
    }
}
