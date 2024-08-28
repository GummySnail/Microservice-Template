namespace Area.Template.Domain.Templates;

public record TemplateId(Guid Value)
{
    public static explicit operator Guid(TemplateId t) =>t.Value;
}