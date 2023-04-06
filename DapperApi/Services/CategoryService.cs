using DapperApi.Data.UnitOfWork;
using DapperApi.Models;
using DapperApi.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace DapperApi.Services
{
    public sealed class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMemoryCache _cache;

        public CategoryService(IUnitOfWork unitOfWork, IMemoryCache cache)
        {
            _unitOfWork = unitOfWork;
            _cache = cache;
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            var categories = await _unitOfWork.Category.GetCategoriesAsync();

            if (categories.Any())
            {
                return categories;
            }

            return Enumerable.Empty<Category>();
        }

        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            var categories = await _unitOfWork.Category.GetAllCategoriesAsync();

            if (categories.Any())
            {
                return categories;
            }

            return Enumerable.Empty<Category>();
        }

        public async Task<Category?> GetCategory(long id)
        {
            if (_cache.TryGetValue($"CategoryId={id}", out Category? category))
            {
                return category;
            }


            category = await _unitOfWork.Category.GetCategoryAsync(id);

            if (category != null)
            {
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                                            .SetSlidingExpiration(TimeSpan.FromSeconds(60))
                                            .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
                                            .SetPriority(CacheItemPriority.Normal)
                                            .SetSize(1024);

                _cache.Set($"CategoryId={category.Id}", category, cacheEntryOptions);

                return category;
            }

            return null;
        }

        public async Task<IEnumerable<Product>> GetCategoryProducts(long id)
        {
            var category = await _unitOfWork.Category.GetCategoryAsync(id);

            if (category != null)
            {
                return category.Products;
            }

            return Enumerable.Empty<Product>();
        }

        public async Task<int> CreateCategory(Category category)
        {
            var changes = await _unitOfWork.Category.CreateCategoryAsync(category);

            return changes;
        }

        public async Task<int> UpdateCategory(Category category)
        {
            var changes = await _unitOfWork.Category.UpdateCategoryAsync(category);

            return changes;
        }

        public async Task<int> DeleteCategory(long id)
        {
            var changes = await _unitOfWork.Category.DeleteCategoryAsync(id);

            return changes;
        }
    }
}
