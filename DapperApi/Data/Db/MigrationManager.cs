using DapperApi.Migrations.Dapper;
using FluentMigrator.Runner;

namespace DapperApi.Data.Db
{
    public static class MigrationManager
    {
        public static WebApplication MigrateDatabase(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var dbService = scope.ServiceProvider.GetRequiredService<Database>();
                var migrationService = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();

                try
                {
                    dbService.CreateDatabase("DapperMigrationExample");

                    migrationService.ListMigrations();
                    migrationService.MigrateUp();
                    //migrationService.MigrateDown(202106280001);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            return app;
        }
    }
}
