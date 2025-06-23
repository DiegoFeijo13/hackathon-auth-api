using API.Controllers;
using Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace UnitTests.API.Token;

public class GetTokenTests
{
    private readonly Mock<ISender> _senderMock;
    private readonly TokenController _sut;

    public GetTokenTests()
    {
        _senderMock = new Mock<ISender>();
        _sut = new TokenController(_senderMock.Object);
    }

    [Fact]
    public async Task GetToken_InformadoDadosValidos_DeverRetornarOk()
    {
        var nome = "user";
        var senha = "password";
        var expectedToken = "generatedToken";

        _senderMock
            .Setup(m => m.Send(It.IsAny<ListarTokenQuery>(), default))
            .ReturnsAsync(expectedToken);

        var result = await _sut.GetToken(nome, senha);

        var okResult = Assert.IsType<OkObjectResult>(result);
    }


    [Fact]
    public async Task GetToken_InformadoDadosInvalidos_DeverRetornarUnauthorized()
    {
        var nome = "testUser";
        var senha = "testPassword";

        _senderMock
            .Setup(m => m.Send(It.IsAny<ListarTokenQueryHandler>(), default))
            .ReturnsAsync((string?)null);        

        var result = await _sut.GetToken(nome, senha);

        Assert.IsType<UnauthorizedResult>(result);
    }
}