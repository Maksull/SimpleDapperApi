using AutoMapper;
using DapperApi.AutoMapper;
using DapperApi.Endpoints;
using DapperApi.Models.Dto.Product;
using DapperApi.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DapperApi.Tests.Endpoints
{
    public sealed class ProductsEndpointsTests
    {
        private readonly Mock<IProductService> _productService;
        private readonly IMapper _mapper;

        public ProductsEndpointsTests()
        {
            _mapper = GetMapper();

            _productService = new();
        }

        #region GetProducts

        [Fact]
        public void GetProducts_WhenCalled_ReturnOk()
        {
            //Arrange
            List<Product> products = new()
            {
                new Product
                {
                    Id = 1,
                    Name = "Ball",
                    Description = "The best size and weight",
                    Price = 19.50m,
                    CategoryId = 1,
                },
                new Product
                {
                    Id = 2,
                    Name = "Corner Flags",
                    Description = "Give your playing field a professional touch",
                    Price = 34.95m,
                    CategoryId = 2,
                }
            };

            _productService.Setup(rep => (rep.GetProducts().Result)).Returns(products.AsEnumerable());

            //Act
            var response = ProductsEndpoints.GetProducts(_productService.Object).Result;
            var result = response as Ok<IEnumerable<Product>>;

            //Assert
            result.Should().BeOfType<Ok<IEnumerable<Product>>>();
            result.Value.Should().BeOfType<List<Product>>();
            result.Value.Should().NotBeEmpty();
            result.Value.Should().BeEquivalentTo(products);
        }

        [Fact]
        public void GetProducts_WhenCalled_ReturnNotFound()
        {
            //Arrange
            _productService.Setup(rep => (rep.GetProducts().Result)).Returns(Enumerable.Empty<Product>());

            //Act
            var response = ProductsEndpoints.GetProducts(_productService.Object).Result;
            var result = response as NotFound;

            //Assert
            result.Should().BeOfType<NotFound>();
        }

        [Fact]
        public void GetProducts_WhenException_ReturnProblem()
        {
            //Arrange
            _productService.Setup(rep => rep.GetProducts().Result)
                .Throws(new Exception("Test Exception"));

            //Act
            var response = ProductsEndpoints.GetProducts(_productService.Object).Result;
            var result = response as ProblemHttpResult;

            //Assert
            result.Should().BeOfType<ProblemHttpResult>();
            result.Should().Match<ProblemHttpResult>(r => r.StatusCode == StatusCodes.Status500InternalServerError
                                                  && r.ProblemDetails.Detail == "Test Exception");
        }

        #endregion


        #region GetProduct

        [Fact]
        public void GetProduct_WhenCalled_ReturnOk()
        {
            //Arrange
            Product product = new()
            {
                Id = 1,
                Name = "Ball",
                Description = "The best size and weight",
                Price = 19.50m,
                CategoryId = 1,
            };

            _productService.Setup(repo => repo.GetProduct(It.IsAny<long>()).Result).Returns(product);

            //Act
            var response = ProductsEndpoints.GetProduct(1, _productService.Object).Result;
            var result = response as Ok<Product>;

            //Assert
            result.Value.Should().BeOfType<Product>();
            result.Value.Should().BeEquivalentTo(product);
        }

        [Fact]
        public void GetProduct_WhenCalled_ReturnNotFound()
        {
            //Arrange
            _productService.Setup(repo => repo.GetProduct(It.IsAny<long>()).Result).Returns((Product)null);

            //Act
            var response = ProductsEndpoints.GetProduct(1, _productService.Object).Result;
            var result = response as NotFound;

            //Assert
            result.Should().BeOfType<NotFound>();
        }

        [Fact]
        public void GetProduct_WhenException_ReturnProblem()
        {
            //Arrange
            _productService.Setup(rep => rep.GetProduct(It.IsAny<long>()).Result)
                .Throws(new Exception("Test Exception"));

            //Act
            var response = ProductsEndpoints.GetProduct(1, _productService.Object).Result;
            var result = response as ProblemHttpResult;

            //Assert
            result.Should().BeOfType<ProblemHttpResult>();
            result.Should().Match<ProblemHttpResult>(r => r.StatusCode == StatusCodes.Status500InternalServerError
                                                  && r.ProblemDetails.Detail == "Test Exception");
        }

        #endregion


        #region GetProduct

        [Fact]
        public void GetProductCategory_WhenCalled_ReturnOk()
        {
            //Arrange
            Category category = new()
            {
                Id = 1,
                Name = "Ball",
            };

            _productService.Setup(repo => repo.GetProductCategory(It.IsAny<long>()).Result).Returns(category);

            //Act
            var response = ProductsEndpoints.GetProductCategory(1, _productService.Object).Result;
            var result = response as Ok<Category>;

            //Assert
            result.Value.Should().BeOfType<Category>();
            result.Value.Should().BeEquivalentTo(category);
        }

        [Fact]
        public void GetProductCategory_WhenCalled_ReturnNotFound()
        {
            //Arrange
            _productService.Setup(repo => repo.GetProductCategory(It.IsAny<long>()).Result).Returns((Category)null);

            //Act
            var response = ProductsEndpoints.GetProductCategory(1, _productService.Object).Result;
            var result = response as NotFound;

            //Assert
            result.Should().BeOfType<NotFound>();
        }

        [Fact]
        public void GetProductCategory_WhenException_ReturnProblem()
        {
            //Arrange
            _productService.Setup(rep => rep.GetProductCategory(It.IsAny<long>()).Result)
                .Throws(new Exception("Test Exception"));

            //Act
            var response = ProductsEndpoints.GetProductCategory(1, _productService.Object).Result;
            var result = response as ProblemHttpResult;

            //Assert
            result.Should().BeOfType<ProblemHttpResult>();
            result.Should().Match<ProblemHttpResult>(r => r.StatusCode == StatusCodes.Status500InternalServerError
                                                  && r.ProblemDetails.Detail == "Test Exception");
        }

        #endregion


        #region CreateProduct

        [Fact]
        public void CreateProduct_WhenCalled_ReturnOk()
        {
            //Arrange
            ProductInsertDto newProduct = new()
            {
                Name = "Corner Flags",
                Description = "Give your playing field a professional touch",
                Price = 34.95m,
                CategoryId = 2,
            };

            _productService.Setup(rep => rep.CreateProduct(It.IsAny<Product>()).Result).Returns(1);

            //Act
            var response = ProductsEndpoints.CreateProduct(newProduct, _productService.Object, _mapper).Result;
            var result = response as Ok;

            //Assert
            result.Should().BeOfType<Ok>();
        }

        [Fact]
        public void CreateProduct_WhenCalled_ReturnBadRequest()
        {
            //Arrange
            ProductInsertDto newProduct = new()
            {
                Name = "Corner Flags",
                Description = "Give your playing field a professional touch",
                Price = 34.95m,
                CategoryId = 2,
            };
            _productService.Setup(rep => rep.CreateProduct(It.IsAny<Product>()).Result).Returns(0);



            //Act
            var response = ProductsEndpoints.CreateProduct(newProduct, _productService.Object, _mapper).Result;
            var result = response as BadRequest;

            //Assert
            result.Should().BeOfType<BadRequest>();
        }

        [Fact]
        public void CreateProduct_WhenException_ReturnProblem()
        {
            //Arrange
            ProductInsertDto newProduct = new()
            {
                Name = "Corner Flags",
                Description = "Give your playing field a professional touch",
                Price = 34.95m,
                CategoryId = 2,
            };
            _productService.Setup(rep => rep.CreateProduct(It.IsAny<Product>()).Result)
                .Throws(new Exception("Test Exception"));

            //Act
            var response = ProductsEndpoints.CreateProduct(newProduct, _productService.Object, _mapper).Result;
            var result = response as ProblemHttpResult;

            //Assert
            result.Should().BeOfType<ProblemHttpResult>();
            result.Should().Match<ProblemHttpResult>(r => r.StatusCode == StatusCodes.Status500InternalServerError
                                                  && r.ProblemDetails.Detail == "Test Exception");
        }

        #endregion


        #region UpdateProduct

        [Fact]
        public void UpdateProduct_WhenCalled_ReturnOk()
        {
            //Arrange
            ProductUpdateDto newProduct = new()
            {
                Id = 2,
                Name = "Corner Flags",
                Description = "Give your playing field a professional touch",
                Price = 34.95m,
                CategoryId = 2,
            };
            _productService.Setup(rep => rep.UpdateProduct(It.IsAny<Product>()).Result).Returns(1);

            //Act
            var response = ProductsEndpoints.UpdateProduct(newProduct, _productService.Object, _mapper).Result;
            var result = response as Ok;

            //Assert
            result.Should().BeOfType<Ok>();
        }

        [Fact]
        public void UpdateProduct_WhenCalled_ReturnNotFound()
        {
            //Arrange
            ProductUpdateDto newProduct = new()
            {
                Id = 2,
                Name = "Corner Flags",
                Description = "Give your playing field a professional touch",
                Price = 34.95m,
                CategoryId = 2,
            };
            _productService.Setup(rep => rep.UpdateProduct(It.IsAny<Product>()).Result).Returns(0);


            //Act
            var response = ProductsEndpoints.UpdateProduct(newProduct, _productService.Object, _mapper).Result;
            var result = response as NotFound;

            //Assert
            result.Should().BeOfType<NotFound>();
        }

        [Fact]
        public void UpdateProduct_WhenException_ReturnProblem()
        {
            //Arrange
            ProductUpdateDto newProduct = new()
            {
                Id = 2,
                Name = "Corner Flags",
                Description = "Give your playing field a professional touch",
                Price = 34.95m,
                CategoryId = 2,
            };
            _productService.Setup(rep => rep.UpdateProduct(It.IsAny<Product>()).Result)
                .Throws(new Exception("Test Exception"));

            //Act
            var response = ProductsEndpoints.UpdateProduct(newProduct, _productService.Object, _mapper).Result;
            var result = response as ProblemHttpResult;

            //Assert
            result.Should().BeOfType<ProblemHttpResult>();
            result.Should().Match<ProblemHttpResult>(r => r.StatusCode == StatusCodes.Status500InternalServerError
                                                  && r.ProblemDetails.Detail == "Test Exception");
        }

        #endregion


        #region DeleteProduct

        [Fact]
        public void DeleteProduct_WhenCalled_ReturnOk()
        {
            //Arrange
            _productService.Setup(rep => rep.DeleteProduct(1).Result).Returns(1);

            //Act
            var response = ProductsEndpoints.DeleteProduct(1, _productService.Object).Result;
            var result = response as Ok;

            //Assert
            result.Should().BeOfType<Ok>();
        }

        [Fact]
        public void DeleteProduct_WhenCalled_ReturnNotFound()
        {
            //Arrange
            _productService.Setup(rep => rep.DeleteProduct(0).Result).Returns(0);


            //Act
            var response = ProductsEndpoints.DeleteProduct(1, _productService.Object).Result;
            var result = response as NotFound;

            //Assert
            result.Should().BeOfType<NotFound>();
        }

        [Fact]
        public void DeleteProduct_WhenException_ReturnProblem()
        {
            //Arrange
            _productService.Setup(rep => rep.DeleteProduct(It.IsAny<long>()).Result)
                .Throws(new Exception("Test Exception"));

            //Act
            var response = ProductsEndpoints.DeleteProduct(1, _productService.Object).Result;
            var result = response as ProblemHttpResult;

            //Assert
            result.Should().BeOfType<ProblemHttpResult>();
            result.Should().Match<ProblemHttpResult>(r => r.StatusCode == StatusCodes.Status500InternalServerError
                                                  && r.ProblemDetails.Detail == "Test Exception");
        }

        #endregion

        private static IMapper GetMapper()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ProductProfile());
            });

            return mockMapper.CreateMapper();
        }
    }
}
