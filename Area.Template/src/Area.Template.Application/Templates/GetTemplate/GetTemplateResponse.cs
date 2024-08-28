namespace Area.Template.Application.Templates.GetTemplate;

public sealed class GetTemplateResponse
{
    public Guid Id { get; init; }

    public required string Name { get; init; }

    public required string Description { get; init; }
}
