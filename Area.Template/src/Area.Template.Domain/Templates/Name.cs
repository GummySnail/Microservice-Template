namespace Area.Template.Domain.Templates;

public sealed record Name(string Value)
{
    public static explicit operator string(Name name) => name.Value;
}