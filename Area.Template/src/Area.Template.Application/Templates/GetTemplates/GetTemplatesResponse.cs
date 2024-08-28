namespace Area.Template.Application.Templates.GetTemplates;

public sealed class GetTemplatesResponse
{
    public Guid Id { get; init; }

    public required string Name { get; init; }

    public required string Description { get; init; }
}
