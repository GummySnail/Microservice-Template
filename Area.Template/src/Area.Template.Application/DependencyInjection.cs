using FluentValidation;
using Area.Template.Application.Abstractions.Behaviors;
using MediatR.NotificationPublishers;
using Microsoft.Extensions.DependencyInjection;

namespace Area.Template.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);

            // configuration.AddOpenBehavior(typeof(LoggingBehavior<,>));

            configuration.AddOpenBehavior(typeof(ValidationBehavior<,>));

            configuration.NotificationPublisher = new TaskWhenAllPublisher();
        });

        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly, includeInternalTypes: true);

        return services;
    }
}
