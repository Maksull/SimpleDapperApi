using AutoMapper;
using DapperApi.AutoMapper;
using DapperApi.Controllers;
using DapperApi.Models.Dto.Product;
using DapperApi.Services;
using DapperApi.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DapperApi.Tests.Controllers
{
    public sealed class ProductsControllerTests
    {
        private readonly Mock<IProductService> _productService;
        private readonly ProductsController _controller;
        private readonly IMapper _mapper;

        public ProductsControllerTests()
        {
            _productService = new();

            _mapper = GetMapper();

            _controller = new(_productService.Object, _mapper);
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
            var response = _controller.GetProducts().Result;
            var result = response as OkObjectResult;
            var value = (result?.Value as IEnumerable<Product>)?.ToList();

            //Assert
            result.Should().BeOfType<OkObjectResult>();
            value.Should().BeOfType<List<Product>>();
            value.Should().BeEquivalentTo(products);
        }

        [Fact]
        public void GetProducts_WhenCalled_ReturnNotFound()
        {
            //Arrange
            _productService.Setup(rep => (rep.GetProducts().Result)).Returns(Enumerable.Empty<Product>().AsEnumerable());


            //Act
            var response = _controller.GetProducts().Result;
            var result = response as NotFoundResult;

            //Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public void GetProducts_WhenException_ReturnProblem()
        {
            //Arrange
            _productService.Setup(rep => rep.GetProducts().Result)
                .Throws(new Exception("Test Exception"));

            //Act
            var response = _controller.GetProducts().Result as ObjectResult;
            var result = response?.Value as ProblemDetails;

            //Assert
            result.Should().BeOfType<ProblemDetails>();
            result.Should().Match<ProblemDetails>(r => r.Status == StatusCodes.Status500InternalServerError
                                                  && r.Detail == "Test Exception");
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
            var response = _controller.GetProduct(1).Result;
            var result = response as OkObjectResult;
            var value = result?.Value as Product;

            //Assert
            result.Should().BeOfType<OkObjectResult>();
            value.Should().BeOfType<Product>();
            value.Should().BeEquivalentTo(product);
        }

        [Fact]
        public void GetProduct_WhenCalled_ReturnNotFound()
        {
            //Arrange
            _productService.Setup(rep => (rep.GetProduct(It.IsAny<long>()).Result)).Returns((Product)null);

            //Act
            var response = _controller.GetProduct(1).Result;
            var result = response as NotFoundResult;

            //Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public void GetProduct_WhenException_ReturnProblem()
        {
            //Arrange
            _productService.Setup(rep => rep.GetProduct(It.IsAny<long>()).Result)
                .Throws(new Exception("Test Exception"));

            //Act
            var response = _controller.GetProduct(1).Result as ObjectResult;
            var result = response?.Value as ProblemDetails;

            //Assert
            result.Should().BeOfType<ProblemDetails>();
            result.Should().Match<ProblemDetails>(r => r.Status == StatusCodes.Status500InternalServerError
                                                  && r.Detail == "Test Exception");
        }

        #endregion


        #region GetProductCategory

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
            var response = _controller.GetProductCategory(1).Result;
            var result = response as OkObjectResult;
            var value = result?.Value as Category;

            //Assert
            result.Should().BeOfType<OkObjectResult>();
            value.Should().BeOfType<Category>();
            value.Should().BeEquivalentTo(category);
        }

        [Fact]
        public void GetProductCategory_WhenCalled_ReturnNotFound()
        {
            //Arrange
            _productService.Setup(rep => (rep.GetProductCategory(It.IsAny<long>()).Result)).Returns((Category)null);

            //Act
            var response = _controller.GetProductCategory(1).Result;
            var result = response as NotFoundResult;

            //Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public void GetProductCategory_WhenException_ReturnProblem()
        {
            //Arrange
            _productService.Setup(rep => rep.GetProductCategory(It.IsAny<long>()).Result)
                .Throws(new Exception("Test Exception"));

            //Act
            var response = _controller.GetProductCategory(1).Result as ObjectResult;
            var result = response?.Value as ProblemDetails;

            //Assert
            result.Should().BeOfType<ProblemDetails>();
            result.Should().Match<ProblemDetails>(r => r.Status == StatusCodes.Status500InternalServerError
                                                  && r.Detail == "Test Exception");
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
            var response = _controller.CreateProduct(newProduct).Result;
            var result = response as OkResult;

            //Assert
            result.Should().BeOfType<OkResult>();
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
            var response = _controller.CreateProduct(newProduct).Result;

            var result = response as BadRequestResult;

            //Assert
            result.Should().BeOfType<BadRequestResult>();
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
            var response = _controller.CreateProduct(newProduct).Result as ObjectResult;
            var result = response?.Value as ProblemDetails;

            //Assert
            result.Should().BeOfType<ProblemDetails>();
            result.Should().Match<ProblemDetails>(r => r.Status == StatusCodes.Status500InternalServerError
                                                  && r.Detail == "Test Exception");
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
            var response = _controller.UpdateProduct(newProduct).Result;
            var result = response as OkResult;

            //Assert
            result.Should().BeOfType<OkResult>();
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
            var response = _controller.UpdateProduct(newProduct).Result;
            var result = response as NotFoundResult;

            //Assert
            result.Should().BeOfType<NotFoundResult>();
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
            var response = _controller.UpdateProduct(newProduct).Result as ObjectResult;
            var result = response?.Value as ProblemDetails;

            //Assert
            result.Should().BeOfType<ProblemDetails>();
            result.Should().Match<ProblemDetails>(r => r.Status == StatusCodes.Status500InternalServerError
                                                  && r.Detail == "Test Exception");
        }

        #endregion


        #region DeleteProduct

        [Fact]
        public void DeleteProduct_WhenCalled_ReturnOk()
        {
            //Arrange
            _productService.Setup(rep => rep.DeleteProduct(1).Result).Returns(1);

            //Act
            var response = _controller.DeleteProduct(1).Result;
            var result = response as OkResult;

            //Assert
            result.Should().BeOfType<OkResult>();
        }

        [Fact]
        public void DeleteProduct_WhenCalled_ReturnNotFound()
        {
            //Arrange
            _productService.Setup(rep => rep.DeleteProduct(0).Result).Returns(0);


            //Act
            var response = _controller.DeleteProduct(1).Result;
            var result = response as NotFoundResult;

            //Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public void DeleteProduct_WhenException_ReturnProblem()
        {
            //Arrange
            _productService.Setup(rep => rep.DeleteProduct(It.IsAny<long>()).Result)
                .Throws(new Exception("Test Exception"));

            //Act
            var response = _controller.DeleteProduct(1).Result as ObjectResult;
            var result = response?.Value as ProblemDetails;

            //Assert
            result.Should().BeOfType<ProblemDetails>();
            result.Should().Match<ProblemDetails>(r => r.Status == StatusCodes.Status500InternalServerError
                                                  && r.Detail == "Test Exception");
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
