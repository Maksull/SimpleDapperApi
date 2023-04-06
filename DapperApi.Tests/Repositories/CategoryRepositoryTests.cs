using DapperApi.Data.Db;
using DapperApi.Data.Repository;
using Microsoft.Extensions.Configuration;

namespace DapperApi.Tests.Repositories
{
    public sealed class CategoryRepositoryTests
    {
        private readonly Mock<DapperContext> _context;
        private readonly CategoryRepository _repository;
        public CategoryRepositoryTests()
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
        public void GetCategories_WhenCalled_ReturnCategories()
        {
            //Arrange

            //Act
            var categories = _repository.GetCategoriesAsync().Result;

            //Assert
            categories.Should().NotBeNullOrEmpty();
            categories.Should().BeOfType<List<Category>>();
        }

        [Fact]
        public void GetAllCategories_WhenCalled_ReturnCategories()
        {
            //Arrange

            //Act
            var categories = _repository.GetAllCategoriesAsync().Result;

            //Assert
            categories.Should().NotBeNullOrEmpty();
            categories.Should().BeOfType<List<Category>>();
        }

        [Fact]
        public void GetCategory_WhenCalled_ReturnCategory()
        {
            //Arrange
            long searchedId = 1;

            //Act
            var category = _repository.GetCategoryAsync(searchedId).Result;

            //Assert
            category.Should().BeOfType<Category>();
            category.Id.Should().Be(searchedId);
        }

        [Fact]
        public void GetCategory_WhenCalled_ReturnNull()
        {
            //Arrange
            long searchedId = 0;

            //Act
            var category = _repository.GetCategoryAsync(searchedId).Result;

            //Assert
            category.Should().BeNull();
        }

        [Fact]
        public void CreateCategory_WhenCalled_ReturnAffectedRows()
        {
            //Arrange
            Category newCategory = new()
            {
                Id = 0,
                Name = "TestName",
            };

            //Act
            var affectedRows = _repository.CreateCategoryAsync(newCategory).Result;

            //Assert
            affectedRows.Should().BeOfType(typeof(int));
            affectedRows.Should().Be(1);
        }

        [Fact]
        public void UpdateCategory_WhenCalled_ReturnAffectedRows()
        {
            //Arrange
            Category newCategory = new()
            {
                Id = 5,
                Name = "TestUpdated",
            };

            //Act
            var affectedRows = _repository.UpdateCategoryAsync(newCategory).Result;

            //Assert
            affectedRows.Should().BeOfType(typeof(int));
            affectedRows.Should().Be(1);
        }

        [Fact]
        public void DeleteCategory_WhenCalled_ReturnAffectedRows()
        {
            //Arrange

            //Act
            var affectedRows = _repository.DeleteCategoryAsync(4).Result;

            //Assert
            affectedRows.Should().BeOfType(typeof(int));
            affectedRows.Should().Be(1);
        }
    }
}
