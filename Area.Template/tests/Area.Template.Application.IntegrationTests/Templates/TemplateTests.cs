using Area.Template.Application.IntegrationTests.Infrastructure;
using Area.Template.Application.Templates.AddTemplate;
using FluentAssertions;

namespace Area.Template.Application.IntegrationTests.Templates;

public class TemplateTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    [Fact]
    public async Task AddTemplateCommand_ShouldCtreateTemplate()
    {
        var name = $"Test Context Name {Guid.NewGuid()}";
        var description = "Test Context Description";
        var command = new AddTemplateCommand(name, description);

        var id = await Sender.Send(command);

        var item = DbContext.Templates.FirstOrDefault(c => (Guid)c.Id == id.Value);
        
        item.Should().NotBeNull();
        item.Name.Value.Should().Be(name);
        item.Description.Value.Should().Be(description);
        item.Id.Value.Should().NotBe(Guid.Empty);
    }
}