using Area.Template.Application.Abstractions.Clock;
using Area.Template.Application.Templates.GetTemplates;
using Area.Template.Domain.Abstractions.Contracts;
using Area.Template.Domain.Templates;
using FluentAssertions;
using NSubstitute;

namespace Area.Template.Application.UnitTests.Templates;

public class GetTemplatesTests
{
    private static readonly DateTime UtcNow = DateTime.UtcNow;
    private static readonly GetTemplatesQuery Query = new();

    private readonly GetTemplatesQueryHandler _handler;
    private readonly ITemplateRepository _repositoryMock;
    private readonly IUnitOfWork _unitOfWorkMock;

    public GetTemplatesTests()
    {
        _repositoryMock = Substitute.For<ITemplateRepository>();
        _unitOfWorkMock = Substitute.For<IUnitOfWork>();

        var dateTimeProviderMock = Substitute.For<IDateTimeProvider>();
        dateTimeProviderMock.UtcNow.Returns(UtcNow);

        _handler = new GetTemplatesQueryHandler(_repositoryMock);
    }

    [Fact]
    public async Task Handle_Should_CallRepository()
    {
        // Arrange
        var list = Substitute.For<IReadOnlyList<TemplateModel>>();
        _repositoryMock.GetAllAsync(Arg.Any<CancellationToken>()).Returns(list);
        
        // Act
        await _handler.Handle(Query, default);

        // Assert
        _repositoryMock.Received(1);
    }

    [Fact]
    public async Task Handle_Should_ReturnMappedData()
    {
        // Arrange
        var modelA = TemplateModel.Create(new Name("Name A"), new Description("Description A"));
        var modelB = TemplateModel.Create(new Name("Name B"), new Description("Description B"));
        var models = new List<TemplateModel> {modelA, modelB}.AsReadOnly();
        
        _repositoryMock.GetAllAsync(Arg.Any<CancellationToken>()).Returns(models);
        
        // Act
        var result = await _handler.Handle(Query, default);

        // Assert
        result.Value.Should().HaveCount(models.Count);
    }
}
