using API.Controllers;
using Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace UnitTests.API.Token;

public class GetClienteTokenTests
{
    private readonly Mock<ISender> _senderMock;
    private readonly TokenController _sut;

    public GetClienteTokenTests()
    {
        _senderMock = new Mock<ISender>();
        _sut = new TokenController(_senderMock.Object);
    }

    [Fact]
    public async Task GetClienteToken_InformadoDadosValidos_DeverRetornarOk()
    {
        var cpf = "00000000000";
        var senha = "password";
        var expectedToken = "generatedToken";

        _senderMock
            .Setup(m => m.Send(It.IsAny<GetClienteTokenQuery>(), default))
            .ReturnsAsync(expectedToken);

        var result = await _sut.GetClienteToken(cpf, senha);

        var okResult = Assert.IsType<OkObjectResult>(result);
    }


    [Fact]
    public async Task GetClienteToken_InformadoDadosInvalidos_DeverRetornarUnauthorized()
    {
        var cpf = "";
        var senha = "";

        _senderMock
            .Setup(m => m.Send(It.IsAny<GetClienteTokenQueryHandler>(), default))
            .ReturnsAsync((string?)null);        

        var result = await _sut.GetClienteToken(cpf, senha);

        Assert.IsType<UnauthorizedResult>(result);
    }
}