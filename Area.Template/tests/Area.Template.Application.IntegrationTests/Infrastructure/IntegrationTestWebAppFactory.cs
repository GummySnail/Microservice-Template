using Area.Template.Api;
using Area.Template.Application.Abstractions.Data;
using Area.Template.Infrastructure;
using Area.Template.Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.MsSql;

namespace Area.Template.Application.IntegrationTests.Infrastructure;

public class IntegrationTestWebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly MsSqlContainer _dbContainer = new MsSqlBuilder()
        .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
        .WithPassword("Passw0rd")
        .Build();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            var descriptorType = typeof(DbContextOptions<ApplicationDbContext>);
            var descriptor = services.SingleOrDefault(s => s.ServiceType == descriptorType);

            if (descriptor != null)
            {
                services.Remove(descriptor);
            }
           
            var connectionString = $"{_dbContainer.GetConnectionString()};Pooling=False";

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

            var sqlFactoryDescriptor = services.SingleOrDefault(s => s.ServiceType == typeof(ISqlConnectionFactory));
            if (sqlFactoryDescriptor != null) services.Remove(sqlFactoryDescriptor);

            services.AddSingleton<ISqlConnectionFactory>(_ => new SqlConnectionFactory(connectionString));
        });
    }

    public Task InitializeAsync()
    {
        return _dbContainer.StartAsync();
    }

    public new Task DisposeAsync()
    {
        return _dbContainer.StopAsync();
    }
}
