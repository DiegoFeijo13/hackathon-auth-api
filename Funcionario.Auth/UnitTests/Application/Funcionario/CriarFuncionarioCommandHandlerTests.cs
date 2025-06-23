using Application.Commands;
using Core.Enums;
using Core.Interfaces;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Moq;

namespace UnitTests.Application.Funcionario;
public class CriarFuncionarioCommandHandlerTests
{
    private readonly Mock<IFuncionarioRepository> _repositoryMock;
    private readonly Mock<IPublisher> _publisherMock;
    private readonly CriarFuncionarioCommandHandler _sut;

    public CriarFuncionarioCommandHandlerTests()
    {
        _repositoryMock = new Mock<IFuncionarioRepository>();
        _publisherMock = new Mock<IPublisher>();
        _sut = new CriarFuncionarioCommandHandler(_repositoryMock.Object, _publisherMock.Object);
    }

    [Fact]
    public async Task Handle_InformadosDadosValidos_DeveAdicionarUsuario()
    {
        var command = new CriarFuncionarioCommand("nome", "email@test.com", "senhagrande", FuncionarioFuncao.Atendente);

        _repositoryMock
            .Setup(x => x.CriarFuncionarioAsync(It.IsAny<FuncionarioEntity>()))
            .ReturnsAsync(new FuncionarioEntity { Id = Guid.NewGuid()});

        var result = await _sut.Handle(command, CancellationToken.None);

        _repositoryMock.Verify(x => x.CriarFuncionarioAsync(It.IsAny<FuncionarioEntity>()), Times.Once);
    }

    [Fact]
    public async Task Handle_InformadosDadosInvalidos_ValidationException()
    {
        var command = new CriarFuncionarioCommand("", "", "", FuncionarioFuncao.Atendente);

        await Assert.ThrowsAsync<ValidationException>(async () => await _sut.Handle(command, CancellationToken.None));
    }
}
