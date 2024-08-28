using Area.Template.Application.Abstractions.Messaging;

namespace Area.Template.Application.Templates.GetTemplates;

public sealed record GetTemplatesQuery() : IQuery<IReadOnlyList<GetTemplatesResponse>>;
