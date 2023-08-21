using System.Data.SqlClient;
using System.Data;

public class DapperContext
{
    private readonly IConfiguration configuration;
    private readonly string? connectionString;

    public DapperContext(IConfiguration configuration)
    {
        this.configuration = configuration;
        this.connectionString = this.configuration.GetConnectionString("SqlConnection");
    }

    public IDbConnection CreateConnection()
        => new SqlConnection(connectionString);
}