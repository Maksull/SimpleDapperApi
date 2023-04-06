using Dapper;
using DapperApi.Data.Db;
using DapperApi.Data.Repository.Interfaces;
using DapperApi.Models;

namespace DapperApi.Data.Repository
{
    public sealed class CategoryRepository : ICategoryRepository
    {
        private readonly DapperContext _context;
        public CategoryRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            const string query = @"SELECT * FROM ""Categories"" c JOIN ""Products"" p ON (c.""Id"" = p.""CategoryId"")";
            using var connection = _context.GetConnection();
            var categories = new Dictionary<long, Category>();
            var result = await connection.QueryAsync<Category, Product, Category>(
                query,
                (category, product) =>
                {
                    if (!categories.TryGetValue(category.Id, out var currentCategory))
                    {
                        currentCategory = category;
                        currentCategory.Products = new List<Product>();
                        categories.Add(currentCategory.Id, currentCategory);
                    }
                    currentCategory.Products.Add(product);
                    return currentCategory;
                }
            );
            return categories.Values;
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            string query = @"SELECT * FROM ""Categories""";
            using (var connection = _context.GetConnection())
            {
                var categories = await connection.QueryAsync<Category>(query);

                return categories;
            }
        }

        public async Task<Category?> GetCategoryAsync(long id)
        {
            const string query = @"SELECT * FROM ""Categories"" AS c JOIN ""Products"" p ON (c.""Id"" = p.""CategoryId"")  WHERE c.""Id"" = @Id ";
            using var connection = _context.GetConnection();
            var categories = new Dictionary<long, Category>();
            var result = await connection.QueryAsync<Category, Product, Category>(
                query,
                (category, product) =>
                {
                    if (!categories.TryGetValue(category.Id, out var currentCategory))
                    {
                        currentCategory = category;
                        currentCategory.Products = new List<Product>();
                        categories.Add(currentCategory.Id, currentCategory);
                    }
                    currentCategory.Products.Add(product);
                    return currentCategory;
                },
                new { Id = id }
            );
            return categories.Values.FirstOrDefault();
        }

        public async Task<int> CreateCategoryAsync(Category product)
        {
            string query = @"INSERT INTO ""Categories"" (""Name"") VALUES (@Name)";
            using (var connection = _context.GetConnection())
            {
                var changes = await connection.ExecuteAsync(query, new
                {
                    product.Name,
                });

                return changes;
            }
        }

        public async Task<int> UpdateCategoryAsync(Category product)
        {
            string query = @"UPDATE ""Categories"" SET ""Name"" = @Name WHERE ""Id"" = @Id";
            using (var connection = _context.GetConnection())
            {
                var changes = await connection.ExecuteAsync(query, new
                {
                    product.Id,
                    product.Name,
                });

                return changes;
            }
        }

        public async Task<int> DeleteCategoryAsync(long id)
        {
            string query = @"DELETE FROM ""Categories"" WHERE ""Id"" = @Id";
            using (var connection = _context.GetConnection())
            {
                var changes = await connection.ExecuteAsync(query, new { Id = id });

                return changes;
            }
        }

    }
}
