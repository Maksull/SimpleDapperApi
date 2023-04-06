using DapperApi.Data.UnitOfWork;
using DapperApi.Models;
using DapperApi.Services;
using Microsoft.Extensions.Caching.Memory;

namespace DapperApi.Tests.Services
{
    public sealed class ProductServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly MemoryCache _cache;
        private readonly ProductService _productService;

        public ProductServiceTests()
        {
            _unitOfWork = new();
            _cache = new(new MemoryCacheOptions());
            _productService = new(_unitOfWork.Object, _cache);
        }


        #region GetProducts

        [Fact]
        public void GetProducts_WhenCalled_ReturnProducts()
        {
            //Arrange
            List<Product> products = new()
            {
                new()
                {
                    Id = 1,
                    Name = "First",
                    Price = 1,
                    CategoryId = 1,
                },
                new()
                {
                    Id = 2,
                    Name = "Second",
                    Price = 2,
                    CategoryId = 2,
                }
            };

            _unitOfWork.Setup(u => u.Product.GetProductsAsync().Result).Returns(products.AsQueryable());

            //Act
            var result = _productService.GetProducts().Result.ToList();

            //Assert
            result.Should().BeOfType<List<Product>>();
            result.Should().BeEquivalentTo(products);
        }

        [Fact]
        public void GetProducts_WhenThereIsCache_ReturnProducts()
        {
            //Arrange
            List<Product> products = new()
            {
                new()
                {
                    Id = 1,
                    Name = "First",
                    Price = 1,
                    CategoryId = 1,
                },
                new()
                {
                    Id = 2,
                    Name = "Second",
                    Price = 2,
                    CategoryId = 2,
                }
            };

            _unitOfWork.Setup(u => u.Product.GetProductsAsync().Result).Returns(products.AsQueryable());
            _cache.Set("productsList", products);

            //Act
            var result = _productService.GetProducts().Result.ToList();

            //Assert
            result.Should().BeOfType<List<Product>>();
            result.Should().BeEquivalentTo(products);
        }

        [Fact]
        public void GetProducts_WhenCalled_ReturnEmpty()
        {
            //Arrange
            _unitOfWork.Setup(u => u.Product.GetProductsAsync().Result).Returns(Enumerable.Empty<Product>());

            //Act
            var result = _productService.GetProducts().Result.ToList();

            //Assert
            result.Should().BeEmpty();
        }

        #endregion


        #region GetProduct

        [Fact]
        public void GetProduct_WhenCalled_ReturnProduct()
        {
            //Arrange
            Product product = new()
            {
                Id = 1,
                Name = "First",
                Price = 1,
                CategoryId = 1,
            };

            _unitOfWork.Setup(u => u.Product.GetProductAsync(It.IsAny<long>()).Result).Returns(product);

            //Act
            var result = _productService.GetProduct(1).Result;

            //Assert
            result.Should().BeOfType<Product>();
            result.Should().BeEquivalentTo(product);
        }

        [Fact]
        public void GetProduct_WhenThereIsCache_ReturnProduct()
        {
            //Arrange
            Product product = new()
            {
                Id = 1,
                Name = "First",
                Price = 1,
                CategoryId = 1,
            };

            _unitOfWork.Setup(u => u.Product.GetProductAsync(It.IsAny<long>()).Result).Returns(product);
            _cache.Set($"ProductId={1}", product);

            //Act
            var result = _productService.GetProduct(1).Result;

            //Assert
            result.Should().BeOfType<Product>();
            result.Should().BeEquivalentTo(product);
        }

        [Fact]
        public void GetProduct_WhenCalled_ReturnNull()
        {
            //Arrange
            _unitOfWork.Setup(u => u.Product.GetProductAsync(It.IsAny<long>()).Result).Returns((Product)null);

            //Act
            var result = _productService.GetProduct(1).Result;

            //Assert
            result.Should().BeNull();
        }

        #endregion


        #region GetProductCategory

        [Fact]
        public void GetProductCategory_WhenCalled_ReturnCategory()
        {
            //Arrange
            Product product = new()
            {
                Id = 1,
                Name = "First",
                Price = 1,
                CategoryId = 1,
            };
            Category category = new()
            {
                Id = 1,
                Name = "First",
            };

            _unitOfWork.Setup(u => u.Product.GetProductAsync(It.IsAny<long>()).Result).Returns(product);
            _unitOfWork.Setup(u => u.Category.GetCategoryAsync(It.IsAny<long>()).Result).Returns(category);


            //Act
            var result = _productService.GetProductCategory(1).Result;

            //Assert
            result.Should().BeOfType<Category>();
            result.Should().BeEquivalentTo(category);
        }

        [Fact]
        public void GetProductCategory_WhenThereIsProductCache_ReturnCategory()
        {
            //Arrange
            Product product = new()
            {
                Id = 1,
                Name = "First",
                Price = 1,
                CategoryId = 1,
            };
            Category category = new()
            {
                Id = 1,
                Name = "First",
            };

            _unitOfWork.Setup(u => u.Product.GetProductAsync(It.IsAny<long>()).Result).Returns(product);
            _unitOfWork.Setup(u => u.Category.GetCategoryAsync(It.IsAny<long>()).Result).Returns(category);
            _cache.Set($"ProductId={1}", product);

            //Act
            var result = _productService.GetProductCategory(1).Result;

            //Assert
            result.Should().BeOfType<Category>();
            result.Should().BeEquivalentTo(category);
        }

        [Fact]
        public void GetProductCategory_WhenThereIsCategoryCache_ReturnCategory()
        {
            //Arrange
            Product product = new()
            {
                Id = 1,
                Name = "First",
                Price = 1,
                CategoryId = 1,
            };
            Category category = new()
            {
                Id = 1,
                Name = "First",
            };

            _unitOfWork.Setup(u => u.Product.GetProductAsync(It.IsAny<long>()).Result).Returns(product);
            _unitOfWork.Setup(u => u.Category.GetCategoryAsync(It.IsAny<long>()).Result).Returns(category);
            _cache.Set($"ProductId={1}", product);
            _cache.Set($"CategoryId={1}", category);

            //Act
            var result = _productService.GetProductCategory(1).Result;

            //Assert
            result.Should().BeOfType<Category>();
            result.Should().BeEquivalentTo(category);
        }

        [Fact]
        public void GetProductCategory_WhenThereIsCache_ReturnNull()
        {
            //Arrange
            Product product = new()
            {
                Id = 1,
                Name = "First",
                Price = 1,
                CategoryId = 1,
            };
            Category category = new()
            {
                Id = 1,
                Name = "First",
            };

            _unitOfWork.Setup(u => u.Product.GetProductAsync(It.IsAny<long>()).Result).Returns(product);
            _unitOfWork.Setup(u => u.Category.GetCategoryAsync(It.IsAny<long>()).Result).Returns((Category)null);
            _cache.Set($"ProductId={1}", product);

            //Act
            var result = _productService.GetProductCategory(1).Result;

            //Assert
            result.Should().BeNull();
        }


        [Fact]
        public void GetProductCategory_WhenCalled_IfProduct_Is_Null_ReturnNull()
        {
            //Arrange
            Product product = new()
            {
                Id = 1,
                Name = "First",
                Price = 1,
                CategoryId = 1,
            };

            _unitOfWork.Setup(u => u.Product.GetProductAsync(It.IsAny<long>()).Result).Returns(product);
            _unitOfWork.Setup(u => u.Category.GetCategoryAsync(It.IsAny<long>()).Result).Returns((Category)null);


            //Act
            var result = _productService.GetProductCategory(1).Result;

            //Assert
            result.Should().BeNull();
        }

        [Fact]
        public void GetProductCategory_WhenCalled_IfCategory_Is_Null_ReturnNull()
        {
            //Arrange
            _unitOfWork.Setup(u => u.Product.GetProductAsync(It.IsAny<long>()).Result).Returns((Product)null);

            //Act
            var result = _productService.GetProduct(1).Result;

            //Assert
            result.Should().BeNull();
        }

        #endregion


        #region CreateProduct

        [Fact]
        public void CreateProduct_WhenCalled_ReturnNumberOfChanges()
        {
            //Arrange
            Product product = new()
            {
                Id = 1,
                Name = "First",
                Price = 1,
                CategoryId = 1,
            };

            _unitOfWork.Setup(u => u.Product.CreateProductAsync(It.IsAny<Product>())).Returns(Task.FromResult(1));

            //Act
            var result = _productService.CreateProduct(product).Result;

            //Assert
            result.Should().BeOfType(typeof(int));
        }

        #endregion


        #region UpdateProduct

        [Fact]
        public void UpdateProduct_WhenCalled_IfProductExist_ReturnNumberOfChanges()
        {
            //Arrange
            Product product = new()
            {
                Id = 1,
                Name = "First",
                Price = 1,
                CategoryId = 1,
            };

            _unitOfWork.Setup(u => u.Product.UpdateProductAsync(It.IsAny<Product>())).Returns(Task.FromResult(1));

            //Act
            var result = _productService.UpdateProduct(product).Result;

            //Assert
            result.Should().BeOfType(typeof(int));
        }

        #endregion


        #region DeleteProduct

        [Fact]
        public void DeleteProduct_WhenCalled_IfProductExist_ReturnNumberOfChanges()
        {
            //Arrange
            Product product = new()
            {
                Id = 1,
                Name = "First",
                Price = 1,
                CategoryId = 1,
            };

            _unitOfWork.Setup(u => u.Product.DeleteProductAsync(It.IsAny<long>())).Returns(Task.FromResult(1));

            //Act
            var result = _productService.DeleteProduct(1).Result;

            //Assert
            result.Should().BeOfType(typeof(int));
        }

        #endregion

    }
}
