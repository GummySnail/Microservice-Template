using Area.Template.Domain.Abstractions;

namespace Area.Template.Domain.Templates;

public static class Errors
{
    public static readonly Error NotFound = new(
        "Template.NotFound",
        "Template with the specified identifier was not found");

    public static readonly Error NotCreated = new(
        "Template.NotCreated",
        "The template wasn't created");
    
    public static readonly Error AlreadyExists = new(
        "Template.AlreadyExists",
        "The template already exists");
}