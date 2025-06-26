using API.Controllers;
using Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace UnitTests.API.Token;

public class GetFuncionarioTokenTests
{
    private readonly Mock<ISender> _senderMock;
    private readonly TokenController _sut;

    public GetFuncionarioTokenTests()
    {
        _senderMock = new Mock<ISender>();
        _sut = new TokenController(_senderMock.Object);
    }

    [Fact]
    public async Task GetFuncionarioToken_InformadoDadosValidos_DeverRetornarOk()
    {
        var email = "user@test.com";
        var senha = "password";
        var expectedToken = "generatedToken";

        _senderMock
            .Setup(m => m.Send(It.IsAny<GetFuncionarioTokenQuery>(), default))
            .ReturnsAsync(expectedToken);

        var result = await _sut.GetFuncionarioToken(email, senha);

        var okResult = Assert.IsType<OkObjectResult>(result);
    }


    [Fact]
    public async Task GetFuncionarioToken_InformadoDadosInvalidos_DeverRetornarUnauthorized()
    {
        var nome = "";
        var senha = "";

        _senderMock
            .Setup(m => m.Send(It.IsAny<GetFuncionarioTokenQueryHandler>(), default))
            .ReturnsAsync((string?)null);        

        var result = await _sut.GetFuncionarioToken(nome, senha);

        Assert.IsType<UnauthorizedResult>(result);
    }
}