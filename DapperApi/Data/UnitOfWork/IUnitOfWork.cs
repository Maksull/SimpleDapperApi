using DapperApi.Data.Repository.Interfaces;

namespace DapperApi.Data.UnitOfWork
{
    public interface IUnitOfWork
    {
        IProductRepository Product { get; }
        ICategoryRepository Category { get; }
    }
}
