using Area.Template.Application.Abstractions.Messaging;

namespace Area.Template.Application.Templates.GetTemplate;

public sealed record GetTemplateQuery(Guid Id) : IQuery<GetTemplateResponse>
{
    public string CacheKey => $"templates-{Id}";

    public TimeSpan? Expiration => null;
}
