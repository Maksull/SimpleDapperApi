using AutoMapper;
using DapperApi.AutoMapper;
using DapperApi.Controllers;
using DapperApi.Endpoints;
using DapperApi.Models.Dto.Category;
using DapperApi.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DapperApi.Tests.Controllers
{
    public sealed class CategoriesControllerTests
    {
        private readonly Mock<ICategoryService> _categoryService;
        private readonly IMapper _mapper;
        private readonly CategoriesController _controller;

        public CategoriesControllerTests()
        {
            _categoryService = new();

            _mapper = GetMapper();

            _controller = new(_categoryService.Object, _mapper);
        }

        #region GetCategories

        [Fact]
        public void GetCategories_WhenCalled_ReturnOk()
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
                },
            };

            _categoryService.Setup(rep => rep.GetCategories().Result).Returns(categories);

            //Act
            var response = _controller.GetCategories().Result;
            var result = response as OkObjectResult;
            var value = (result?.Value as IEnumerable<Category>)?.ToList();

            //Assert
            result.Should().BeOfType<OkObjectResult>();
            value.Should().BeOfType<List<Category>>();
            value.Should().BeEquivalentTo(categories);
        }

        [Fact]
        public void GetCategories_WhenCalled_ReturnNotFound()
        {
            //Arrange
            _categoryService.Setup(rep => rep.GetCategories().Result).Returns(Array.Empty<Category>().AsQueryable());

            //Act
            var response = _controller.GetCategories().Result;
            var result = response as NotFoundResult;

            //Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public void GetCategories_WhenException_ReturnProblem()
        {
            //Arrange
            _categoryService.Setup(rep => rep.GetCategories().Result)
                .Throws(new Exception("Test Exception"));

            //Act
            var response = _controller.GetCategories().Result as ObjectResult;
            var result = response?.Value as ProblemDetails;

            //Assert
            result.Should().BeOfType<ProblemDetails>();
            result.Should().Match<ProblemDetails>(r => r.Status == StatusCodes.Status500InternalServerError
                                                  && r.Detail == "Test Exception");
        }

        #endregion


        #region GetAllCategories

        [Fact]
        public void GetAllCategories_WhenCalled_ReturnOk()
        {
            //Arrange
            List<Category> categories = new()
            {
                new()
                {
                    Id = 1,
                    Name = "Ball",
                },
                new()
                {
                    Id = 2,
                    Name = "Corner Flags",
                }
            };

            _categoryService.Setup(rep => (rep.GetAllCategories().Result)).Returns(categories.AsEnumerable());

            //Act
            var response = _controller.GetAllCategories().Result;
            var result = response as OkObjectResult;
            var value = (result?.Value as IEnumerable<Category>)?.ToList();

            //Assert
            result.Should().BeOfType<OkObjectResult>();
            value.Should().BeOfType<List<Category>>();
            value.Should().BeEquivalentTo(categories);
        }

        [Fact]
        public void GetAllCategories_WhenCalled_ReturnNotFound()
        {
            //Arrange
            _categoryService.Setup(rep => (rep.GetAllCategories().Result)).Returns(Enumerable.Empty<Category>().AsEnumerable());

            //Act
            var response = _controller.GetAllCategories().Result;
            var result = response as NotFoundResult;

            //Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public void GetAllCategories_WhenException_ReturnProblem()
        {
            //Arrange
            _categoryService.Setup(rep => rep.GetAllCategories().Result)
                .Throws(new Exception("Test Exception"));

            //Act
            var response = _controller.GetAllCategories().Result as ObjectResult;
            var result = response?.Value as ProblemDetails;

            //Assert
            result.Should().BeOfType<ProblemDetails>();
            result.Should().Match<ProblemDetails>(r => r.Status == StatusCodes.Status500InternalServerError
                                                  && r.Detail == "Test Exception");
        }

        #endregion


        #region GetCategory

        [Fact]
        public void GetCategory_WhenCalled_ReturnOk()
        {
            //Arrange
            Category category = new()
            {
                Id = 1,
                Name = "First",
            };

            _categoryService.Setup(rep => rep.GetCategory(It.IsAny<long>()).Result).Returns(category);

            //Act
            var response = _controller.GetCategory(1).Result;
            var result = response as OkObjectResult;
            var value = result?.Value as Category;

            //Assert
            result.Should().BeOfType<OkObjectResult>();
            value.Should().BeOfType<Category>();
            value.Should().BeEquivalentTo(category);
        }

        [Fact]
        public void GetCategory_WhenCalled_ReturnNotFound()
        {
            //Arrange
            _categoryService.Setup(rep => rep.GetCategory(It.IsAny<long>()).Result).Returns((Category)null);

            //Act
            var response = _controller.GetCategory(1).Result;
            var result = response as NotFoundResult;

            //Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public void GetCategory_WhenException_ReturnProblem()
        {
            //Arrange
            _categoryService.Setup(rep => rep.GetCategory(It.IsAny<long>()).Result)
                .Throws(new Exception("Test Exception"));

            //Act
            var response = _controller.GetCategory(1).Result as ObjectResult;
            var result = response?.Value as ProblemDetails;

            //Assert
            result.Should().BeOfType<ProblemDetails>();
            result.Should().Match<ProblemDetails>(r => r.Status == StatusCodes.Status500InternalServerError
                                                  && r.Detail == "Test Exception");
        }

        #endregion


        #region GetCategoryProducts

        [Fact]
        public void GetCategoryProducts_WhenCalled_ReturnOk()
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

            _categoryService.Setup(repo => repo.GetCategoryProducts(It.IsAny<long>()).Result).Returns(products.AsEnumerable());

            //Act
            var response = _controller.GetCategoryProducts(1).Result;
            var result = response as OkObjectResult;
            var value = result?.Value as List<Product>;

            //Assert
            value.Should().BeOfType<List<Product>>();
            value.Should().BeEquivalentTo(products);
        }

        [Fact]
        public void GetCategoryProducts_WhenCalled_ReturnNotFound()
        {
            //Arrange
            _categoryService.Setup(repo => repo.GetCategoryProducts(It.IsAny<long>()).Result).Returns(Enumerable.Empty<Product>());

            //Act
            var response = _controller.GetCategoryProducts(1).Result;
            var result = response as NotFoundResult;

            //Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public void GetCategoryProducts_WhenException_ReturnProblem()
        {
            //Arrange
            _categoryService.Setup(rep => rep.GetCategoryProducts(It.IsAny<long>()).Result)
                .Throws(new Exception("Test Exception"));

            //Act
            var response = _controller.GetCategoryProducts(1).Result as ObjectResult;
            var result = response?.Value as ProblemDetails;

            //Assert
            result.Should().BeOfType<ProblemDetails>();
            result.Should().Match<ProblemDetails>(r => r.Status == StatusCodes.Status500InternalServerError
                                                  && r.Detail == "Test Exception");
        }

        #endregion


        #region CreateCategory

        [Fact]
        public void CreateCategory_WhenCalled_ReturnOk()
        {
            //Arrange
            CategoryInsertDto insertedCategory = new()
            {
                Name = "First"
            };

            _categoryService.Setup(rep => rep.CreateCategory(It.IsAny<Category>()).Result).Returns(1);

            //Act
            var response = _controller.CreateCategory(insertedCategory).Result;
            var result = response as OkResult;

            //Assert
            result.Should().BeOfType<OkResult>();
        }

        [Fact]
        public void CreateCategory_WhenCalled_ReturnBadRequest()
        {
            //Arrange
            CategoryInsertDto insertedCategory = new()
            {
                Name = "First"
            };
            _categoryService.Setup(rep => rep.CreateCategory(It.IsAny<Category>()).Result).Returns(0);

            //Act
            var response = _controller.CreateCategory(insertedCategory).Result;
            var result = response as BadRequestResult;

            //Assert
            result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public void CreateCategory_WhenException_ReturnProblem()
        {
            //Arrange
            CategoryInsertDto insertedCategory = new()
            {
                Name = "First"
            };
            _categoryService.Setup(rep => rep.CreateCategory(It.IsAny<Category>()).Result)
                .Throws(new Exception("Test Exception"));

            //Act
            var response = _controller.CreateCategory(insertedCategory).Result as ObjectResult;
            var result = response?.Value as ProblemDetails;

            //Assert
            result.Should().BeOfType<ProblemDetails>();
            result.Should().Match<ProblemDetails>(r => r.Status == StatusCodes.Status500InternalServerError
                                                  && r.Detail == "Test Exception");
        }

        #endregion


        #region UpdateCategory

        [Fact]
        public void UpdateCategory_WhenCalled_ReturnOk()
        {
            //Arrange
            CategoryUpdateDto insertedCategory = new()
            {
                Id = 1,
                Name = "First"
            };

            _categoryService.Setup(rep => rep.UpdateCategory(It.IsAny<Category>()).Result).Returns(1);

            //Act
            var response = _controller.UpdateCategory(insertedCategory).Result;
            var result = response as OkResult;

            //Assert
            result.Should().BeOfType<OkResult>();
        }

        [Fact]
        public void UpdateCategory_WhenCalled_ReturnNotFound()
        {
            //Arrange
            CategoryUpdateDto insertedCategory = new()
            {
                Id = 1,
                Name = "First"
            };

            _categoryService.Setup(rep => rep.UpdateCategory(It.IsAny<Category>()).Result).Returns(0);

            //Act
            var response = _controller.UpdateCategory(insertedCategory).Result;
            var result = response as NotFoundResult;

            //Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public void UpdateCategory_WhenException_ReturnProblem()
        {
            //Arrange
            CategoryUpdateDto insertedCategory = new()
            {
                Id = 1,
                Name = "First"
            };
            _categoryService.Setup(rep => rep.UpdateCategory(It.IsAny<Category>()).Result)
                .Throws(new Exception("Test Exception"));

            //Act
            var response = _controller.UpdateCategory(insertedCategory).Result as ObjectResult;
            var result = response?.Value as ProblemDetails;

            //Assert
            result.Should().BeOfType<ProblemDetails>();
            result.Should().Match<ProblemDetails>(r => r.Status == StatusCodes.Status500InternalServerError
                                                  && r.Detail == "Test Exception");
        }

        #endregion


        #region DeleteCategory

        [Fact]
        public void DeleteCategory_WhenCalled_ReturnOk()
        {
            //Arrange
            _categoryService.Setup(rep => rep.DeleteCategory(1).Result).Returns(1);

            //Act
            var response = _controller.DeleteCategory(1).Result;
            var result = response as OkResult;

            //Assert
            result.Should().BeOfType<OkResult>();
        }

        [Fact]
        public void DeleteCategory_WhenCalled_ReturnNotFound()
        {
            //Arrange
            _categoryService.Setup(rep => rep.DeleteCategory(1).Result).Returns(0);

            //Act
            var response = _controller.DeleteCategory(1).Result;
            var result = response as NotFoundResult;

            //Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public void DeleteCategory_WhenException_ReturnProblem()
        {
            //Arrange
            _categoryService.Setup(rep => rep.DeleteCategory(It.IsAny<long>()).Result)
                .Throws(new Exception("Test Exception"));

            //Act
            var response = _controller.DeleteCategory(1).Result as ObjectResult;
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
                cfg.AddProfile(new CategoryProfile());
            });
            return mockMapper.CreateMapper();
        }
    }
}
