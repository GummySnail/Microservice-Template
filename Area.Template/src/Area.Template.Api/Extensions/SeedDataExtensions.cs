using System.Data;
using Area.Template.Application.Abstractions.Data;
using Area.Template.Domain.Shared;
using Area.Template.Domain.Templates;

namespace Area.Template.Api.Extensions;

internal static class SeedDataExtensions
{
    internal static void SeedData(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();

        ISqlConnectionFactory sqlConnectionFactory = scope.ServiceProvider.GetRequiredService<ISqlConnectionFactory>();
        using IDbConnection connection = sqlConnectionFactory.CreateConnection();

        var defaultTemplate = TemplateModel.Create(new Name(Constants.DEFAULT_TEMPLATE_NAME), 
            new Description(Constants.DEFAULT_TEMPLATE_DESCRIPTION));

        var sql = $"""
            {GenerateAddTemplateSql(defaultTemplate)}
            """;

        ConnectionState originalState = connection.State;
        if (originalState != ConnectionState.Open)
        { 
            connection.Open();
        }

        try
        {
            IDbCommand command = connection.CreateCommand();
            command.CommandText = sql;
            command.ExecuteScalar();
        }
        finally
        {
            if (originalState == ConnectionState.Closed)
            {
                connection.Close();
            }
        }
    }

    private static string GenerateAddTemplateSql(TemplateModel item)
    {
        return $"""
                    IF NOT EXISTS (SELECT 1 FROM [dbo].[Templates] WHERE [Name] = '{item.Name.Value}')
                    BEGIN
                           INSERT INTO [dbo].[Templates] (Id, Name, Description)
                           VALUES ('{item.Id.Value}', '{item.Name.Value}', '{item.Description.Value}')
                    END;
                """;
    }
}
