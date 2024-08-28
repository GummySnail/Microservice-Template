using Area.Template.Application.Abstractions.Clock;
using Asp.Versioning;
using Area.Template.Application.Abstractions.Data;
using Area.Template.Domain.Abstractions.Contracts;
using Area.Template.Domain.Templates;
using Area.Template.Infrastructure.Repositories;
using Area.Template.Infrastructure.Clock;
using Area.Template.Infrastructure.Data;
using JifitiHttpHelper.Extensions;
using JifitiHttpHelper.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Area.Template.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructurePersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IDateTimeProvider, DateTimeProvider>();

            AddPersistence(services, configuration);

            return services;
        }

        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IDateTimeProvider, DateTimeProvider>();

            AddPersistence(services, configuration);

            AddHealthChecks(services, configuration);

            AddApiVersioning(services);

            return services;
        }

        private static void AddPersistence(IServiceCollection services, IConfiguration configuration)
        {
            services.AddMemoryCache();
            services.AddJifitiHttpHelper(configuration);
            services.AddSingleton<IHttpClientHelper, HttpClientHelper>();
            
            string connectionString = configuration.GetConnectionString("DB") ?? throw new ArgumentNullException(nameof(configuration));

            services.AddSqlServer<ApplicationDbContext>(connectionString)
                .AddScoped<ITemplateRepository, TemplateRepository>();

            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());

            services.AddSingleton<ISqlConnectionFactory>(_ => new SqlConnectionFactory(connectionString));
        }

        private static void AddHealthChecks(IServiceCollection services, IConfiguration configuration)
        {
            services.AddHealthChecks()
                .AddSqlServer(configuration.GetConnectionString("DB")!);
        }

        private static void AddApiVersioning(IServiceCollection services)
        {
            services
                .AddApiVersioning(options =>
                {
                    options.DefaultApiVersion = new ApiVersion(1);
                    options.ReportApiVersions = true;
                    options.ApiVersionReader = new UrlSegmentApiVersionReader();
                    options.AssumeDefaultVersionWhenUnspecified = true;
                })
                .AddApiExplorer(options =>
                {
                    options.GroupNameFormat = "'v'V";
                    options.SubstituteApiVersionInUrl = true;
                });
        }
    }
}
