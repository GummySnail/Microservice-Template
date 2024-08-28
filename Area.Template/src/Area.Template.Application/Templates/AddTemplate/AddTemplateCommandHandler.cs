using Area.Template.Application.Abstractions.Messaging;
using Area.Template.Application.Exceptions;
using Area.Template.Domain.Abstractions;
using Area.Template.Domain.Abstractions.Contracts;
using Area.Template.Domain.Templates;

namespace Area.Template.Application.Templates.AddTemplate;

internal sealed class AddTemplateCommandHandler(ITemplateRepository repository, IUnitOfWork unitOfWork)
    : ICommandHandler<AddTemplateCommand, Guid>
{
    public async Task<Result<Guid>> Handle(AddTemplateCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var newItem = TemplateModel.Create(new Name(request.Name), new Description(request.Description));
            repository.Add(newItem);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return newItem.Id.Value;
        }
        catch (ConcurrencyException)
        {
            return Result.Failure<Guid>(Errors.NotCreated);
        }
    }
}

