using Microsoft.Data.SqlClient;

namespace E7BeautyShop.AgendaService.Infra.Data.Connection;

public interface IConnectionDb : IDisposable
{
    public SqlConnection Connection { get; }
    void Open();
    void Close();
}