using Area.Template.Api.Endpoints.Templates;
using Area.Template.Api.Extensions;
using HealthChecks.UI.Client;
using Area.Template.Application;
using Area.Template.Infrastructure;
using JifitiDotLogger.Extensions;
using JifitiDotLogger.Loggers;
using JifitiMatrix.Extensions;
using JifitiObservability.Extensions;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using ILogger = JifitiDotLogger.Interfaces.ILogger;

namespace Area.Template.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddApplication();
            builder.Services.AddInfrastructure(builder.Configuration);
            
            builder.Services.AddObservability(builder.Configuration);
            builder.Services.AddJifitiDotLogger(builder.Configuration);
            builder.Services.AddSingleton<ILogger, AsyncSerilogLogger>();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                
                app.ApplyMigrations();
                app.SeedData();
            }

            // app.UseHttpsRedirection();
            
            // app.UseRequestContextLogging();
            
            app.UseCustomExceptionHandler();

            app.UsePathBase("/area");
            app.UseVersionController(context: "/area/api");

            app.MapHealthChecks("health", new HealthCheckOptions
            {
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            app.MapTemplateEndpoints();

            app.Run();
        }
    }
}
