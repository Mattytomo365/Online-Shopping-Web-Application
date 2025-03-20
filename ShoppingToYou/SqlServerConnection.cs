using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace Database
{
    public class SqlServerConnection : ISqlServerConnection
    {
        private readonly IConfiguration Configuration;

        public SqlServerConnection(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public SqlConnection GetConnection
        {
            get
            {
                return new SqlConnection(Configuration.GetConnectionString("DefaultConnection"));
            }
        }
    }
}
