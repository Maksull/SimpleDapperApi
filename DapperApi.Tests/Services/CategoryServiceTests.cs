using DapperApi.Data.UnitOfWork;
using DapperApi.Services;
using Microsoft.Extensions.Caching.Memory;

namespace DapperApi.Tests.Services
{
    public sealed class CategoryServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly MemoryCache _cache;
        private readonly CategoryService _categoryService;

        public CategoryServiceTests()
        {
            _unitOfWork = new();
            _cache = new(new MemoryCacheOptions());
            _categoryService = new(_unitOfWork.Object, _cache);
        }


        #region GetCategories

        [Fact]
        public void GetCategories_WhenCalled_ReturnCategories()
        {
            //Arrange
            List<Category> categories = new()
            {
                new()
                {
                    Id = 1,
                    Name = "First",
                },
                new()
                {
                    Id = 2,
                    Name = "Second",
                }
            };

            _unitOfWork.Setup(u => u.Category.GetCategoriesAsync().Result).Returns(categories.AsQueryable());

            //Act
            var result = _categoryService.GetCategories().Result.ToList();

            //Assert
            result.Should().BeOfType<List<Category>>();
            result.Should().BeEquivalentTo(categories);
        }

        [Fact]
        public void GetCategories_WhenCalled_ReturnEmpty()
        {
            //Arrange
            _unitOfWork.Setup(u => u.Category.GetCategoriesAsync().Result).Returns(Enumerable.Empty<Category>());

            //Act
            var result = _categoryService.GetCategories().Result.ToList();

            //Assert
            result.Should().BeEmpty();
        }

        #endregion


        #region GetAllCategories

        [Fact]
        public void GetAllCategories_WhenCalled_ReturnCategories()
        {
            //Arrange
            List<Category> categories = new()
            {
                new()
                {
                    Id = 1,
                    Name = "First",
                },
                new()
                {
                    Id = 2,
                    Name = "Second",
                }
            };

            _unitOfWork.Setup(u => u.Category.GetAllCategoriesAsync().Result).Returns(categories.AsQueryable());

            //Act
            var result = _categoryService.GetAllCategories().Result.ToList();

            //Assert
            result.Should().BeOfType<List<Category>>();
            result.Should().BeEquivalentTo(categories);
        }

        [Fact]
        public void GetAllCategories_WhenCalled_ReturnEmpty()
        {
            //Arrange
            _unitOfWork.Setup(u => u.Category.GetAllCategoriesAsync().Result).Returns(Enumerable.Empty<Category>());

            //Act
            var result = _categoryService.GetAllCategories().Result.ToList();

            //Assert
            result.Should().BeEmpty();
        }

        #endregion


        #region GetCategory

        [Fact]
        public void GetCategory_WhenCalled_ReturnCategory()
        {
            //Arrange
            Category category = new()
            {
                Id = 1,
                Name = "First",
            };

            _unitOfWork.Setup(u => u.Category.GetCategoryAsync(It.IsAny<long>()).Result).Returns(category);

            //Act
            var result = _categoryService.GetCategory(1).Result;

            //Assert
            result.Should().BeOfType<Category>();
            result.Should().BeEquivalentTo(category);
        }

        [Fact]
        public void GetCategory_WhenThereIsCache_ReturnCategory()
        {
            //Arrange
            Category category = new()
            {
                Id = 1,
                Name = "First",
            };

            _unitOfWork.Setup(u => u.Category.GetCategoryAsync(It.IsAny<long>()).Result).Returns(category);
            _cache.Set($"CategoryId={1}", category);

            //Act
            var result = _categoryService.GetCategory(1).Result;

            //Assert
            result.Should().BeOfType<Category>();
            result.Should().BeEquivalentTo(category);
        }

        [Fact]
        public void GetCategory_WhenCalled_ReturnNull()
        {
            //Arrange
            _unitOfWork.Setup(u => u.Category.GetCategoryAsync(It.IsAny<long>()).Result).Returns((Category)null);

            //Act
            var result = _categoryService.GetCategory(1).Result;

            //Assert
            result.Should().BeNull();
        }

        #endregion


        #region GetCategoryProducts

        [Fact]
        public void GetCategoryProducts_WhenCalled_ReturnCategory()
        {
            //Arrange
            Category category = new()
            {
                Id = 1,
                Name = "First",
                Products = new()
                {
                    new()
                    {
                        Id = 1,
                        Name = "First",
                        Description = "Description",
                        Price = 1,
                        CategoryId = 1
                    }
                }
            };

            _unitOfWork.Setup(u => u.Category.GetCategoryAsync(It.IsAny<long>()).Result).Returns(category);

            //Act
            var result = _categoryService.GetCategoryProducts(1).Result.ToList();

            //Assert
            result.Should().BeOfType<List<Product>>();
            result.Should().BeEquivalentTo(category.Products);
        }

        [Fact]
        public void GetCategoryProducts_WhenCalled_ReturnEmpty()
        {
            //Arrange
            _unitOfWork.Setup(u => u.Category.GetCategoryAsync(It.IsAny<long>()).Result).Returns((Category)null);

            //Act
            var result = _categoryService.GetCategoryProducts(1).Result;

            //Assert
            result.Should().BeEmpty();
        }

        #endregion


        #region CreateCategory

        [Fact]
        public void CreateCategory_WhenCalled_ReturnNumberOfChanges()
        {
            //Arrange
            Category category = new()
            {
                Id = 1,
                Name = "First",
            };

            _unitOfWork.Setup(u => u.Category.CreateCategoryAsync(It.IsAny<Category>())).Returns(Task.FromResult(1));

            //Act
            var result = _categoryService.CreateCategory(category).Result;

            //Assert
            result.Should().BeOfType(typeof(int));
        }

        #endregion


        #region UpdateCategory

        [Fact]
        public void UpdateCategory_WhenCalled_IfProductExist_ReturnNumberOfChanges()
        {
            //Arrange
            Category category = new()
            {
                Id = 1,
                Name = "First",
            };

            _unitOfWork.Setup(u => u.Category.UpdateCategoryAsync(It.IsAny<Category>())).Returns(Task.FromResult(1));

            //Act
            var result = _categoryService.UpdateCategory(category).Result;

            //Assert
            result.Should().BeOfType(typeof(int));
        }

        #endregion


        #region DeleteCategory

        [Fact]
        public void DeleteCategory_WhenCalled_IfProductExist_ReturnNumberOfChanges()
        {
            //Arrange
            Category category = new()
            {
                Id = 1,
                Name = "First",
            };

            _unitOfWork.Setup(u => u.Category.DeleteCategoryAsync(It.IsAny<long>())).Returns(Task.FromResult(1));

            //Act
            var result = _categoryService.DeleteCategory(1).Result;

            //Assert
            result.Should().BeOfType(typeof(int));
        }

        #endregion
    }
}
