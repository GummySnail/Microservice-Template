using Area.Template.Application.Abstractions.Data;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Area.Template.Infrastructure.Data;

public sealed class SqlConnectionFactory(string connectionString) : ISqlConnectionFactory
{
    public IDbConnection CreateConnection()
    {
        var connection = new SqlConnection(connectionString);
        connection.Open();

        return connection;
    }
}
