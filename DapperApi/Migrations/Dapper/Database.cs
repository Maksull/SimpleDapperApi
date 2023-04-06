using Dapper;
using DapperApi.Data.Db;

namespace DapperApi.Migrations.Dapper
{
    public sealed class Database
    {
        private readonly DapperContext _context;

        public Database(DapperContext context)
        {
            _context = context;
        }

        public void CreateDatabase(string dbName)
        {
            //var query = "SELECT * FROM sys.databases WHERE name = @Name";
            var query = "SELECT datname FROM pg_database WHERE datistemplate = false AND datname = @Name";

            var parameters = new DynamicParameters();
            parameters.Add("Name", dbName);

            using(var connection =  _context.Connection)
            {
                var records = connection.Query(query, parameters);
                if (!records.Any())
                {
                    connection.Execute(@$"CREATE DATABASE ""{dbName}""");
                }
            }
        }
    }
}
