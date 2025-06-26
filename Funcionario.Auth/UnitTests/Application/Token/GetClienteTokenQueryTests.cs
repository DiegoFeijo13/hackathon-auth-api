using Application.Queries;
using Core.Entities;
using Core.Interfaces;
using Moq;

namespace UnitTests.Application.Token;
public class GetClienteTokenQueryTests
{
    private readonly Mock<IClienteRepository> _repositoryMock;
    private readonly Mock<ITokenService> _tokenServiceMock;
    private readonly GetClienteTokenQueryHandler _sut;

    public GetClienteTokenQueryTests()
    {
        _repositoryMock = new Mock<IClienteRepository>();
        _tokenServiceMock = new Mock<ITokenService>();
        _sut = new GetClienteTokenQueryHandler(_repositoryMock.Object, _tokenServiceMock.Object);
    }

    [Fact]
    public async Task Handle_InformadoDadosValidos_DeverRetornarToken()
    {
        var query = new GetClienteTokenQuery("00000000000", "password");
        var expectedToken = "generatedToken";
        

        _repositoryMock.Setup(x => x.GetAsync(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(new ClienteEntity());
        _tokenServiceMock.Setup(x => x.GetClienteToken(It.IsAny<ClienteEntity>()))
            .Returns(expectedToken);

        var result = await _sut.Handle(query, CancellationToken.None);

        Assert.Equal(expectedToken, result);
    }

    [Fact]
    public async Task Handle_InformadoDadosInvalidos_DeverRetornarStringVazio()
    {
        var query = new GetClienteTokenQuery("", "");
        var expectedToken = string.Empty;

        _repositoryMock.Setup(x => x.GetAsync(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(default(ClienteEntity));

        var result = await _sut.Handle(query, CancellationToken.None);

        _tokenServiceMock.Verify(x => x.GetClienteToken(It.IsAny<ClienteEntity>()), Times.Never);

        Assert.Equal(expectedToken, result);
    }
}
