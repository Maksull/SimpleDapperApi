using Dapper;
using DapperApi.Data.Db;
using DapperApi.Data.Repository;
using Microsoft.Extensions.Configuration;
using Moq.Protected;
using System.Data;
using System.Data.Common;

namespace DapperApi.Tests.Repositories
{
    public sealed class ProductRepositoryTests
    {
        private readonly Mock<DapperContext> _context;
        private readonly ProductRepository _repository;

        public ProductRepositoryTests()
        {
            var inMemorySettings = new Dictionary<string, string> {
                {"ConnectionStrings:DapperTestApiPostgres", "Host=localhost;Port=5432;Database=DapperTest;Username=postgres;Password=3459"},
            };

            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings!)
                .Build();

            _context = new(configuration);
            _repository = new(_context.Object);
        }

        [Fact]
        public void GetProducts_WhenCalled_ReturnProducts()
        {
            //Arrange

            //Act
            var products = _repository.GetProductsAsync().Result;

            //Assert
            products.Should().NotBeNullOrEmpty();
            products.Should().BeOfType<List<Product>>();
        }

        [Fact]
        public void GetProduct_WhenCalled_ReturnProduct()
        {
            //Arrange
            long searchedId = 1;

            //Act
            var product = _repository.GetProductAsync(searchedId).Result;

            //Assert
            product.Should().BeOfType<Product>();
            product.Id.Should().Be(searchedId);
        }

        [Fact]
        public void CreateProduct_WhenCalled_ReturnAffectedRows()
        {
            //Arrange
            Product newProduct = new()
            {
                Id = 0,
                Name = "TestName",
                Description = "TestDescription",
                Price = 1m,
                CategoryId = 1,
            };


            //Act
            var affectedRows = _repository.CreateProductAsync(newProduct).Result;

            //Assert
            affectedRows.Should().BeOfType(typeof(int));
            affectedRows.Should().Be(1);
        }

        [Fact]
        public void UpdateProduct_WhenCalled_ReturnAffectedRows()
        {
            //Arrange
            Product newProduct = new()
            {
                Id = 8,
                Name = "Test",
                Description = "TestDescription",
                Price = 1m,
                CategoryId = 1,
            };


            //Act
            var affectedRows = _repository.UpdateProductAsync(newProduct).Result;

            //Assert
            affectedRows.Should().BeOfType(typeof(int));
            affectedRows.Should().Be(1);
        }

        [Fact]
        public void DeleteProduct_WhenCalled_ReturnAffectedRows()
        {
            //Arrange


            //Act
            var affectedRows = _repository.DeleteProductAsync(20).Result;

            //Assert
            affectedRows.Should().BeOfType(typeof(int));
            affectedRows.Should().Be(1);
        }
    }

    public class TestDbConnection<T> : IDbConnection
    {
        private readonly IEnumerable<Product> _data;

        public TestDbConnection(IEnumerable<Product> data)
        {
            _data = data;
        }

        public Task<IEnumerable<Product>> QueryAsync(string sql)
        {
            return Task.FromResult<IEnumerable<Product>>(_data);
        }

        public string ConnectionString { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public int ConnectionTimeout => throw new NotImplementedException();

        public string Database => throw new NotImplementedException();

        public ConnectionState State => throw new NotImplementedException();

        public IDbTransaction BeginTransaction()
        {
            throw new NotImplementedException();
        }

        public IDbTransaction BeginTransaction(IsolationLevel il)
        {
            throw new NotImplementedException();
        }

        public void ChangeDatabase(string databaseName)
        {
            throw new NotImplementedException();
        }

        public void Close()
        {
            throw new NotImplementedException();
        }

        public IDbCommand CreateCommand()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Open()
        {
            throw new NotImplementedException();
        }

        // Implement other IDbConnection methods as needed
    }
}
