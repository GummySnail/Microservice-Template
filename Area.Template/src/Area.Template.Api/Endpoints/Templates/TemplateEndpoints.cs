using Area.Template.Api.Controllers;
using Area.Template.Application.Templates.AddTemplate;
using Area.Template.Application.Templates.GetTemplate;
using Area.Template.Application.Templates.GetTemplates;
using Area.Template.Domain.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Area.Template.Api.Endpoints.Templates;

public static class TemplateEndpoints
{
    public static RouteGroupBuilder MapTemplateEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.NewVersionedApi()
                          .MapGroup("/api/templates")
                          .HasApiVersion(ApiVersions.V1)
                          .WithOpenApi();

        group.MapGet("/", async Task<Ok<IReadOnlyList<GetTemplatesResponse>>>
            (ISender sender, CancellationToken cancellationToken) =>
        {
            var query = new GetTemplatesQuery();
            var result = await sender.Send(query, cancellationToken);

            return TypedResults.Ok(result.Value);
        })
        .WithName("GetAllTemplates")
        .MapToApiVersion(ApiVersions.V1)
        .WithSummary("Gets a list of all templates")
        .WithDescription("Gets a list of all templates");

        group.MapGet("/{id:guid}", async Task<Results<Ok<GetTemplateResponse>, NotFound>>
            (Guid id, ISender sender, CancellationToken cancellationToken) =>
        {
            var query = new GetTemplateQuery(id);
            var result = await sender.Send(query, cancellationToken);

            return result.IsSuccess ? TypedResults.Ok(result.Value) : TypedResults.NotFound();
        })
        .WithName("GetTemplate")
        .MapToApiVersion(ApiVersions.V1)
        .WithSummary("Gets a template by id")
        .WithDescription("Gets the template that has the specified id");

        group.MapPost("/", async Task<Results<CreatedAtRoute<Guid>, BadRequest<Error>>>
            (TemplateRequest request, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = new AddTemplateCommand(request.Name, request.Description);
            var result = await sender.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return TypedResults.BadRequest(result.Error);
            }

            return TypedResults.CreatedAtRoute(result.Value, "AddTemplate", new { id = result.Value });
        })
        .WithName("AddTemplate")
        .MapToApiVersion(ApiVersions.V1)
        .WithSummary("Creates a template")
        .WithDescription("Creates the template that has the specified properties"); ;

        return group;
    }
}