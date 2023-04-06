using DapperApi.Models;

namespace DapperApi.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetProducts();
        Task<Product?> GetProduct(long id);
        Task<Category?> GetProductCategory(long id);
        Task<int> CreateProduct(Product product);
        Task<int?> UpdateProduct(Product product);
        Task<int?> DeleteProduct(long id);
    }
}
