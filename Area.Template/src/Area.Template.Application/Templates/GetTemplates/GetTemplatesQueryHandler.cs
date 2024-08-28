using Area.Template.Application.Abstractions.Messaging;
using Area.Template.Domain.Abstractions;
using Area.Template.Domain.Templates;

namespace Area.Template.Application.Templates.GetTemplates;

internal sealed class GetTemplatesQueryHandler(ITemplateRepository repository)
    : IQueryHandler<GetTemplatesQuery, IReadOnlyList<GetTemplatesResponse>>
{
    public async Task<Result<IReadOnlyList<GetTemplatesResponse>>> Handle(GetTemplatesQuery request, CancellationToken cancellationToken)
    {
        var items = await repository.GetAllAsync(cancellationToken);
                
        return items.Select(c => new GetTemplatesResponse
        {
            Id = c.Id.Value,
            Name = c.Name.Value,
            Description = c.Description.Value
        }).ToList();
    }
}

