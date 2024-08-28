using Area.Template.Domain.Templates;
using FluentValidation;

namespace Area.Template.Application.Templates.AddTemplate;

internal class AddTemplateCommandValidator : AbstractValidator<AddTemplateCommand>
{
    public AddTemplateCommandValidator(ITemplateRepository repository)
    {
        RuleFor(c => c.Name).NotEmpty().MaximumLength(255);
        RuleFor(c => c.Description).NotEmpty().MaximumLength(4000);
        
        RuleFor(x => x.Name)
            .MustAsync(async (name, cancellation) => await repository.GetByNameAsync(name, cancellation) is null)
            .WithMessage(Errors.AlreadyExists.Message);
    }
}
