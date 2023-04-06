using DapperApi.Models;
using Microsoft.EntityFrameworkCore;

namespace DapperApi.Data.Db
{
    public sealed class ApiDataContext : DbContext
    {
        public ApiDataContext(DbContextOptions<ApiDataContext> options) : base(options) { }

        public DbSet<Product> Products => Set<Product>();
        public DbSet<Category> Categories => Set<Category>();
    }
}
