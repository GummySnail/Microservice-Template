using Area.Template.Application.Abstractions.Clock;
using Area.Template.Application.Templates.AddTemplate;
using Area.Template.Domain.Abstractions.Contracts;
using Area.Template.Domain.Templates;
using FluentAssertions;
using NSubstitute;

namespace Area.Template.Application.UnitTests.Templates;

public class AddTemplateTests
{
    private static readonly DateTime UtcNow = DateTime.UtcNow;
    private static readonly AddTemplateCommand Command = new("Test Name", "Test Description");

    private readonly AddTemplateCommandHandler _handler;
    private readonly ITemplateRepository _repositoryMock;

    public AddTemplateTests()
    {
        _repositoryMock = Substitute.For<ITemplateRepository>();
        var unitOfWorkMock = Substitute.For<IUnitOfWork>();

        var dateTimeProviderMock = Substitute.For<IDateTimeProvider>();
        dateTimeProviderMock.UtcNow.Returns(UtcNow);

        _handler = new AddTemplateCommandHandler(_repositoryMock, unitOfWorkMock);
    }

    [Fact]
    public async Task Handle_Should_CallRepository()
    {
        // Act
        await _handler.Handle(Command, default);

        // Assert
        _repositoryMock.Received(1);
    }

    [Fact]
    public async Task Handle_Should_ReturnMappedData()
    {
        // Act
        var result = await _handler.Handle(Command, default);
    
        // Assert
        result.Value.Should().NotBe(Guid.Empty);
    }
}