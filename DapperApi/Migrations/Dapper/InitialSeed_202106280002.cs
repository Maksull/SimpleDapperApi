using DapperApi.Models;
using FluentMigrator;

namespace DapperApi.Migrations.Dapper
{
    [Migration(202106280002)]
    public class InitialSeed_202106280002 : Migration
    {
        public override void Down()
        {
            Delete.FromTable("categories").AllRows();
            Delete.FromTable("products").AllRows();
        }

        public override void Up()
        {
            Insert.IntoTable("categories")
                .Row(new Category{ Id = 1, Name = "First",});
            Insert.IntoTable("categories")
                .Row(new Category { Id = 2, Name = "Second",});
            Insert.IntoTable("categories")
                .Row(new Category { Id = 3, Name = "Third",});


            Insert.IntoTable("products")
                .Row(new Product
                {
                    Id = 1,
                    Name = "Kayak",
                    Description = "A boat for one person",
                    Price = 275,
                    CategoryId = 1,
                });
            Insert.IntoTable("products")
                .Row(new Product
                {
                    Id = 2,
                    Name = "Lifejacket",
                    Description = "Protective and fashionable",
                    Price = 48.95m,
                    CategoryId = 1,
                });
            Insert.IntoTable("products")
                .Row(new Product
                {
                    Id = 3,
                    Name = "Ball",
                    Description = "The best size and weight",
                    Price = 19.50m,
                    CategoryId = 1,
                });
            Insert.IntoTable("products")
                .Row(new Product
                {
                    Id = 4,
                    Name = "Corner Flags",
                    Description = "Give your playing field a professional touch",
                    Price = 34.95m,
                    CategoryId = 2,
                });
            Insert.IntoTable("products")
                .Row(new Product
                {
                    Id = 5,
                    Name = "Stadium",
                    Description = "Flat-packed 35,000-seat stadium",
                    Price = 79500,
                    CategoryId = 2,
                });
            Insert.IntoTable("products")
                .Row(new Product
                {
                    Id = 6,
                    Name = "Thinking Cap",
                    Description = "Improve brain efficiency by 75%",
                    Price = 16,
                    CategoryId = 2,
                });
            Insert.IntoTable("products")
                .Row(new Product
                {
                    Id = 7,
                    Name = "Unsteady Chair",
                    Description = "Secretly give your opponent a disadvantage",
                    Price = 29.95m,
                    CategoryId = 3,
                });
            Insert.IntoTable("products")
                .Row(new Product
                {
                    Id = 8,
                    Name = "Human Chess Board",
                    Description = "A fun game for the family",
                    Price = 75,
                    CategoryId = 3,
                });
            Insert.IntoTable("products")
                .Row(new Product
                {
                    Id = 9,
                    Name = "T-shirt",
                    Description = "Just t-shirt",
                    Price = 1200,
                    CategoryId = 3,
                });
        }
    }
}
