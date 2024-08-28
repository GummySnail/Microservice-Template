using Area.Template.Application.Abstractions.Clock;
using Area.Template.Application.Templates.GetTemplate;
using Area.Template.Domain.Abstractions.Contracts;
using Area.Template.Domain.Templates;
using FluentAssertions;
using NSubstitute;

namespace Area.Template.Application.UnitTests.Templates;

public class GetTemplateTests
{
    private static readonly DateTime UtcNow = DateTime.UtcNow;
    private static readonly GetTemplateQuery Query = new(Guid.NewGuid());

    private readonly GetTemplateQueryHandler _handler;
    private readonly ITemplateRepository _repositoryMock;
    private readonly IUnitOfWork _unitOfWorkMock;

    public GetTemplateTests()
    {
        _repositoryMock = Substitute.For<ITemplateRepository>();
        _unitOfWorkMock = Substitute.For<IUnitOfWork>();

        var dateTimeProviderMock = Substitute.For<IDateTimeProvider>();
        dateTimeProviderMock.UtcNow.Returns(UtcNow);

        _handler = new GetTemplateQueryHandler(_repositoryMock);
    }

    [Fact]
    public async Task Handle_Should_CallRepository()
    {
        // Arrange
        var model = TemplateModel.Create(new Name("Test Name"), new Description("Test Description"));
        _repositoryMock.GetByIdAsync(new TemplateId(Query.Id), Arg.Any<CancellationToken>()).Returns(model);
        
        // Act
        await _handler.Handle(Query, default);

        // Assert
        _repositoryMock.Received(1);
    }

    [Fact]
    public async Task Handle_Should_ReturnMappedData()
    {
        // Arrange
        var model = TemplateModel.Create(new Name("Test Name"), new Description("Test Description"));
        _repositoryMock.GetByIdAsync(new TemplateId(Query.Id), Arg.Any<CancellationToken>()).Returns(model);
        
        // Act
        var result = await _handler.Handle(Query, default);

        // Assert
        result.Value.Id.Should().Be(model.Id.Value);
        result.Value.Name.Should().Be(model.Name.Value);
        result.Value.Description.Should().Be(model.Description.Value);
    }
}