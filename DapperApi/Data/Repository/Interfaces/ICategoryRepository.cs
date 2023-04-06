using DapperApi.Models;

namespace DapperApi.Data.Repository.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetCategoriesAsync();
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
        Task<Category?> GetCategoryAsync(long id);
        Task<int> CreateCategoryAsync(Category product);
        Task<int> UpdateCategoryAsync(Category product);
        Task<int> DeleteCategoryAsync(long id);
    }
}
