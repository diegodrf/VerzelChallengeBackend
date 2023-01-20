using System.Data;
using System.Data.SqlClient;
using WebApi.Services.Interfaces;

namespace WebApi.Services
{
    public class DatabaseService : IDatabaseService
    {
        private string _connectionString;

        public DatabaseService(string connectionString)
        {
            _connectionString = connectionString;
        }
        public IDbConnection CreateConnection() => new SqlConnection(_connectionString);
    }
}
