using Application.Queries;
using Core.Entities;
using Core.Interfaces;
using Moq;

namespace UnitTests.Application.Token;
public class GetFuncionarioTokenQueryTests
{
    private readonly Mock<IFuncionarioRepository> _repositoryMock;
    private readonly Mock<ITokenService> _tokenServiceMock;
    private readonly GetFuncionarioTokenQueryHandler _sut;

    public GetFuncionarioTokenQueryTests()
    {
        _repositoryMock = new Mock<IFuncionarioRepository>();
        _tokenServiceMock = new Mock<ITokenService>();
        _sut = new GetFuncionarioTokenQueryHandler(_repositoryMock.Object, _tokenServiceMock.Object);
    }

    [Fact]
    public async Task Handle_InformadoDadosValidos_DeverRetornarToken()
    {
        var query = new GetFuncionarioTokenQuery("user", "password");
        var expectedToken = "generatedToken";
        

        _repositoryMock.Setup(x => x.GetAsync(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(new FuncionarioEntity());
        _tokenServiceMock.Setup(x => x.GetFuncionarioToken(It.IsAny<FuncionarioEntity>()))
            .Returns(expectedToken);

        var result = await _sut.Handle(query, CancellationToken.None);

        Assert.Equal(expectedToken, result);
    }

    [Fact]
    public async Task Handle_InformadoDadosInvalidos_DeverRetornarStringVazio()
    {
        var query = new GetFuncionarioTokenQuery("user", "password");
        var expectedToken = string.Empty;

        _repositoryMock.Setup(x => x.GetAsync(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(default(FuncionarioEntity));

        var result = await _sut.Handle(query, CancellationToken.None);

        _tokenServiceMock.Verify(x => x.GetFuncionarioToken(It.IsAny<FuncionarioEntity>()), Times.Never);

        Assert.Equal(expectedToken, result);
    }
}
