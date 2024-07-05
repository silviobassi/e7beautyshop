using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace E7BeautyShop.AgendaService.Infra.Connection;

public class SqlServer : IConnectionDb
{
    public SqlConnection Connection { get; }

    public SqlServer(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        Connection = new SqlConnection(connectionString);
    }

    public void Open()
    {
        if (Connection.State != ConnectionState.Open)
        {
            Connection.Open();
        }
    }

    public void Close()
    {
        if (Connection.State != ConnectionState.Closed)
        {
            Connection.Close();
        }
    }

    public void Dispose()
    {
        Connection?.Dispose();
    }
}