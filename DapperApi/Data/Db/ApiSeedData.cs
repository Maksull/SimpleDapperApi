using DapperApi.Models;
using Microsoft.EntityFrameworkCore;

namespace DapperApi.Data.Db
{
    public static class ApiSeedData
    {
        public static void EnsurePopulated(this WebApplication app)
        {
            ApiDataContext context = app.Services.CreateScope().ServiceProvider.GetRequiredService<ApiDataContext>();
            context.Database.Migrate();

            if (context.Products.Any())
            {
                Category c1 = new() { Name = "First" };
                Category c2 = new() { Name = "Second" };
                Category c3 = new() { Name = "Third" };

                context.Categories.AddRange(c1, c2, c3);

                context.Products.AddRange(
                    new Product
                    {
                        Name = "Kayak",
                        Description = "A boat for one person",
                        Price = 275,
                        CategoryId = 1,
                    },
                    new Product
                    {
                        Name = "Lifejacket",
                        Description = "Protective and fashionable",
                        Price = 48.95m,
                        CategoryId = 1,
                    },
                    new Product
                    {
                        Name = "Ball",
                        Description = "The best size and weight",
                        Price = 19.50m,
                        CategoryId = 1,
                    },
                    new Product
                    {
                        Name = "Corner Flags",
                        Description = "Give your playing field a professional touch",
                        Price = 34.95m,
                        CategoryId = 2,
                    },
                    new Product
                    {
                        Name = "Stadium",
                        Description = "Flat-packed 35,000-seat stadium",
                        Price = 79500,
                        CategoryId = 2,
                    },
                    new Product
                    {
                        Name = "Thinking Cap",
                        Description = "Improve brain efficiency by 75%",
                        Price = 16,
                        CategoryId = 2,
                    },
                    new Product
                    {
                        Name = "Unsteady Chair",
                        Description = "Secretly give your opponent a disadvantage",
                        Price = 29.95m,
                        CategoryId = 3,
                    },
                    new Product
                    {
                        Name = "Human Chess Board",
                        Description = "A fun game for the family",
                        Price = 75,
                        CategoryId = 3,
                    },
                    new Product
                    {
                        Name = "T-shirt",
                        Description = "Just t-shirt",
                        Price = 1200,
                        CategoryId = 3,
                    }
                );
            }
            context.SaveChanges();
        }
    }
}
