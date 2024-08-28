using Area.Template.Domain.Abstractions;

namespace Area.Template.Domain.Templates;

public sealed class TemplateModel : Entity<TemplateId>
{
    public TemplateModel(TemplateId id, Name name, Description description) : base(id)
    {
        Name = name;
        Description = description;
    }

    private TemplateModel()
    {
        // do nothing;
    }

    public static TemplateModel Create(Name name, Description description)
    {
        var model = new TemplateModel
        {
            Id = new TemplateId(Guid.NewGuid()),
            Name = name,
            Description = description,
        };

        return model;
    }

    public Name Name { get; private set; }
    
    public Description Description { get; private set; }
}