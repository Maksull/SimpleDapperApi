using FluentMigrator;

namespace DapperApi.Migrations.Dapper
{
    [Migration(202106280001)]
    public sealed class InitialTables_202106280001 : Migration
    {
        public override void Down()
        {
            Delete.Table("products");
            Delete.Table("categories");
        }

        public override void Up()
        {
            Create.Table("categories")
                .WithColumn("Id").AsInt64().PrimaryKey()
                .WithColumn("Name").AsString(50).NotNullable();

            Create.Table("products")
                .WithColumn("Id").AsInt64().PrimaryKey()
                .WithColumn("Name").AsString(50).NotNullable()
                .WithColumn("Description").AsString().NotNullable()
                .WithColumn("Price").AsCurrency().NotNullable()
                .WithColumn("CategoryId").AsInt64().NotNullable().ForeignKey("categories", "Id");
        }
    }
}
