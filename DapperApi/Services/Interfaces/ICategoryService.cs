using DapperApi.Models;

namespace DapperApi.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetCategories();
        Task<IEnumerable<Category>> GetAllCategories();
        Task<Category?> GetCategory(long id);
        Task<IEnumerable<Product>> GetCategoryProducts(long id);
        Task<int> CreateCategory(Category category);
        Task<int> UpdateCategory(Category category);
        Task<int> DeleteCategory(long id);
    }
}
