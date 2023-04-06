using Microsoft.Data.SqlClient;
using Npgsql;
using System.Data;

namespace DapperApi.Data.Db
{
    public class DapperContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        private readonly IDbConnection? _connection = null;

        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DapperTestApiPostgres")!;
        }

        public DapperContext(IDbConnection connection)
        {
            _connection = connection;
        }

        public IDbConnection GetConnection()
        {
            return _connection ?? new NpgsqlConnection(_connectionString);
        }

        //public IDbConnection Connection => new SqlConnection(_connectionString);

        public IDbConnection Connection => new NpgsqlConnection(_connectionString);

    }
}
