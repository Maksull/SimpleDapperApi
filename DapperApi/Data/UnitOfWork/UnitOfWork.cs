using DapperApi.Data.Repository.Interfaces;

namespace DapperApi.Data.UnitOfWork
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly Lazy<IProductRepository> _productRepository;
        private readonly Lazy<ICategoryRepository> _categoryRepository;

        public UnitOfWork(Lazy<IProductRepository> product, Lazy<ICategoryRepository> category)
        {
            _productRepository = product;
            _categoryRepository = category;
        }

        public IProductRepository Product => _productRepository.Value;

        public ICategoryRepository Category => _categoryRepository.Value;
    }
}
