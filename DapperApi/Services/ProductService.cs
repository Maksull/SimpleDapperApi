using DapperApi.Data.UnitOfWork;
using DapperApi.Models;
using DapperApi.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace DapperApi.Services
{
    public sealed class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMemoryCache _cache;

        public ProductService(IUnitOfWork unitOfWork, IMemoryCache cache)
        {
            _unitOfWork = unitOfWork;
            _cache = cache;
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            var products = await _unitOfWork.Product.GetProductsAsync();

            if (products.Any())
            {
                return products;
            }

            return Enumerable.Empty<Product>();
        }

        public async Task<Product?> GetProduct(long id)
        {
            if (_cache.TryGetValue($"ProductId={id}", out Product? product))
            {
                return product;
            }


            product = await _unitOfWork.Product.GetProductAsync(id);

            if (product != null)
            {
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                                            .SetSlidingExpiration(TimeSpan.FromSeconds(60))
                                            .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
                                            .SetPriority(CacheItemPriority.Normal)
                                            .SetSize(1024);

                _cache.Set($"ProductId={product.Id}", product, cacheEntryOptions);

                return product;
            }

            return null;
        }

        public async Task<Category?> GetProductCategory(long id)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                                                        .SetSlidingExpiration(TimeSpan.FromSeconds(60))
                                                        .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
                                                        .SetPriority(CacheItemPriority.Normal)
                                                        .SetSize(1024);

            if (_cache.TryGetValue($"ProductId={id}", out Product? product))
            {
                if (_cache.TryGetValue($"CategoryId={product?.CategoryId}", out Category? category))
                {
                    return category;
                }

                category = await _unitOfWork.Category.GetCategoryAsync(product.CategoryId);

                if (category != null)
                {
                    _cache.Set($"CategoryId={category.Id}", category, cacheEntryOptions);

                    return category;
                }

                return null;
            }


            product = await _unitOfWork.Product.GetProductAsync(id);

            if (product != null)
            {
                _cache.Set($"ProductId={product.Id}", product, cacheEntryOptions);

                var category = await _unitOfWork.Category.GetCategoryAsync(product.CategoryId);

                if (category != null)
                {
                    _cache.Set($"CategoryId={category.Id}", category, cacheEntryOptions);

                    return category;
                }
            }

            return null;
        }

        public async Task<int> CreateProduct(Product product)
        {
            var changes = await _unitOfWork.Product.CreateProductAsync(product);

            return changes;
        }

        public async Task<int?> UpdateProduct(Product product)
        {
            var changes = await _unitOfWork.Product.UpdateProductAsync(product);

            return changes;
        }

        public async Task<int?> DeleteProduct(long id)
        {
            var changes = await _unitOfWork.Product.DeleteProductAsync(id);

            return changes;
        }
    }
}
