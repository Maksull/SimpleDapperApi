using Dapper;
using DapperApi.Data.Db;
using DapperApi.Data.Repository.Interfaces;
using DapperApi.Models;

namespace DapperApi.Data.Repository
{
    public sealed class ProductRepository : IProductRepository
    {
        private readonly DapperContext _context;
        public ProductRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            string query = @"SELECT * FROM ""Products""";
            using (var connection = _context.Connection)
            {
                var products = await connection.QueryAsync<Product>(query);

                return products;
            }
        }

        public async Task<Product> GetProductAsync(long id)
        {
            string query = @"SELECT * FROM ""Products"" WHERE ""Id"" = @Id";
            using (var connection = _context.Connection)
            {
                var product = await connection.QueryFirstOrDefaultAsync<Product>(query, new { Id = id });

                return product;
            }
        }

        public async Task<int> CreateProductAsync(Product product)
        {
            string query = @"INSERT INTO ""Products"" (""Name"", ""Description"", ""Price"", ""CategoryId"") VALUES (@Name, @Description, @Price, @CategoryId)";
            using (var connection = _context.Connection)
            {
                var changes = await connection.ExecuteAsync(query, new
                {
                    product.Name,
                    product.Description,
                    product.Price,
                    product.CategoryId,
                });

                return changes;
            }
        }

        public async Task<int> UpdateProductAsync(Product product)
        {
            string query = @"UPDATE ""Products"" SET ""Name"" = @Name, ""Description"" = @Description, ""Price"" = @Price, ""CategoryId"" = @CategoryId WHERE ""Id"" = @Id";
            using (var connection = _context.Connection)
            {
                var changes = await connection.ExecuteAsync(query, new
                {
                    product.Id,
                    product.Name,
                    product.Description,
                    product.Price,
                    product.CategoryId,
                });

                return changes;
            }
        }

        public async Task<int> DeleteProductAsync(long id)
        {
            string query = @"DELETE FROM ""Products"" WHERE ""Id"" = @Id";
            using (var connection = _context.Connection)
            {
                var changes = await connection.ExecuteAsync(query, new { Id = id });

                return changes;
            }
        }
    }
}
