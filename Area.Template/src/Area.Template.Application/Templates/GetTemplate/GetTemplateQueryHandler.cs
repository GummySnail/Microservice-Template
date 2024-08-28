using Area.Template.Application.Abstractions.Messaging;
using Area.Template.Domain.Abstractions;
using Area.Template.Domain.Templates;

namespace Area.Template.Application.Templates.GetTemplate;

internal sealed class GetTemplateQueryHandler(ITemplateRepository repository)
    : IQueryHandler<GetTemplateQuery, GetTemplateResponse>
{
    public async Task<Result<GetTemplateResponse>> Handle(GetTemplateQuery request, CancellationToken cancellationToken)
    {
        var id = new TemplateId(request.Id);
        var item = await repository.GetByIdAsync(id, cancellationToken);

        if (item is null)
        {
            return Result.Failure<GetTemplateResponse>(Errors.NotFound);
        }

        return new GetTemplateResponse
        {
            Id = item.Id.Value,
            Name = item.Name.Value,
            Description = item.Description.Value
        };
    }
}
