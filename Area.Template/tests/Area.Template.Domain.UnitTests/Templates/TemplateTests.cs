using Area.Template.Domain.Templates;
using FluentAssertions;

namespace Area.Template.Domain.UnitTests.Templates;

public class TemplateTests
{
    [Fact]
    public void Create_Should_SetPropertyValue()
    {
        // Act
        var context = TemplateModel.Create(TemplateData.Name, TemplateData.Description);

        // Assert
        context.Id.Should().NotBe(Guid.Empty);
        context.Name.Should().Be(TemplateData.Name);
        context.Description.Should().Be(TemplateData.Description);        
    }
}
