using Microsoft.Data.SqlClient;

namespace Infrastructure.Abstractions;

public interface ISqlConnectionFactory
{
    SqlConnection CreateSqlConnection();
}
