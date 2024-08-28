using Area.Template.Application.Abstractions.Messaging;

namespace Area.Template.Application.Templates.AddTemplate;

public sealed record AddTemplateCommand(  
    string Name,
    string Description) : ICommand<Guid>;
