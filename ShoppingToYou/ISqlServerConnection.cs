using System.Data.SqlClient;

namespace Database
{
    public interface ISqlServerConnection
    {
        SqlConnection GetConnection { get; }
    }
}
