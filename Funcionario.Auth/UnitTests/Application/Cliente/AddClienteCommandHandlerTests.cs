using Application.Commands;
using Core.Entities;
using Core.Enums;
using Core.Interfaces;
using FluentValidation;
using MediatR;
using Moq;

namespace UnitTests.Application.Cliente;
public class AddClienteCommandHandlerTests
{
    private readonly Mock<IClienteRepository> _repositoryMock;
    private readonly Mock<IPublisher> _publisherMock;
    private readonly AddClienteCommandHandler _sut;

    public AddClienteCommandHandlerTests()
    {
        _repositoryMock = new Mock<IClienteRepository>();
        _publisherMock = new Mock<IPublisher>();
        _sut = new AddClienteCommandHandler(_repositoryMock.Object, _publisherMock.Object);
    }

    [Fact]
    public async Task Handle_InformadosDadosValidos_DeveAdicionarUsuario()
    {
        var command = new AddClienteCommand("nome", "00000000000", "email@test.com", "senhagrande");

        _repositoryMock
            .Setup(x => x.InsertAsync(It.IsAny<ClienteEntity>()))
            .ReturnsAsync(new ClienteEntity { Id = Guid.NewGuid()});

        var result = await _sut.Handle(command, CancellationToken.None);

        _repositoryMock.Verify(x => x.InsertAsync(It.IsAny<ClienteEntity>()), Times.Once);
    }

    [Fact]
    public async Task Handle_InformadosDadosInvalidos_ValidationException()
    {
        var command = new AddClienteCommand("", "", "", "");

        await Assert.ThrowsAsync<ValidationException>(async () => await _sut.Handle(command, CancellationToken.None));
    }
}
