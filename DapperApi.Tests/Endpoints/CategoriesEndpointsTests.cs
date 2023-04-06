using AutoMapper;
using DapperApi.AutoMapper;
using DapperApi.Endpoints;
using DapperApi.Models.Dto.Category;
using DapperApi.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;

namespace DapperApi.Tests.Endpoints
{
    public sealed class CategoriesEndpointsTests
    {
        private readonly Mock<ICategoryService> _categoryService;
        private readonly IMapper _mapper;

        public CategoriesEndpointsTests()
        {
            _mapper = GetMapper();
            _categoryService = new();
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
                    Name = "Ball",
                },
                new()
                {
                    Id = 2,
                    Name = "Corner Flags",
                }
            };

            _categoryService.Setup(rep => (rep.GetCategories().Result)).Returns(categories.AsEnumerable());

            //Act
            var response = CategoriesEndpoints.GetCategories(_categoryService.Object).Result;
            var result = response as Ok<IEnumerable<Category>>;

            //Assert
            result.Should().BeOfType<Ok<IEnumerable<Category>>>();
            result.Value.Should().BeOfType<List<Category>>();
            result.Value.Should().NotBeEmpty();
            result.Value.Should().BeEquivalentTo(categories);
        }

        [Fact]
        public void GetCategories_WhenCalled_ReturnNotFound()
        {
            //Arrange
            _categoryService.Setup(rep => (rep.GetCategories().Result)).Returns(Enumerable.Empty<Category>().AsEnumerable());

            //Act
            var response = CategoriesEndpoints.GetCategories(_categoryService.Object).Result;
            var result = response as NotFound;

            //Assert
            result.Should().BeOfType<NotFound>();
        }

        [Fact]
        public void GetCategories_WhenException_ReturnProblem()
        {
            //Arrange
            _categoryService.Setup(rep => rep.GetCategories().Result)
                .Throws(new Exception("Test Exception"));

            //Act
            var response = CategoriesEndpoints.GetCategories(_categoryService.Object).Result;
            var result = response as ProblemHttpResult;

            //Assert
            result.Should().BeOfType<ProblemHttpResult>();
            result.Should().Match<ProblemHttpResult>(r => r.StatusCode == StatusCodes.Status500InternalServerError
                                                  && r.ProblemDetails.Detail == "Test Exception");
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
            var response = CategoriesEndpoints.GetAllCategories(_categoryService.Object).Result;
            var result = response as Ok<IEnumerable<Category>>;

            //Assert
            result.Should().BeOfType<Ok<IEnumerable<Category>>>();
            result.Value.Should().BeOfType<List<Category>>();
            result.Value.Should().NotBeEmpty();
            result.Value.Should().BeEquivalentTo(categories);
        }

        [Fact]
        public void GetAllCategories_WhenCalled_ReturnNotFound()
        {
            //Arrange
            _categoryService.Setup(rep => (rep.GetAllCategories().Result)).Returns(Enumerable.Empty<Category>().AsEnumerable());

            //Act
            var response = CategoriesEndpoints.GetAllCategories(_categoryService.Object).Result;
            var result = response as NotFound;

            //Assert
            result.Should().BeOfType<NotFound>();
        }

        [Fact]
        public void GetAllCategories_WhenException_ReturnProblem()
        {
            //Arrange
            _categoryService.Setup(rep => rep.GetAllCategories().Result)
                .Throws(new Exception("Test Exception"));

            //Act
            var response = CategoriesEndpoints.GetAllCategories(_categoryService.Object).Result;
            var result = response as ProblemHttpResult;

            //Assert
            result.Should().BeOfType<ProblemHttpResult>();
            result.Should().Match<ProblemHttpResult>(r => r.StatusCode == StatusCodes.Status500InternalServerError
                                                  && r.ProblemDetails.Detail == "Test Exception");
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
                Name = "Ball",
            };

            _categoryService.Setup(repo => repo.GetCategory(It.IsAny<long>()).Result).Returns(category);

            //Act
            var response = CategoriesEndpoints.GetCategory(1, _categoryService.Object).Result;
            var result = response as Ok<Category>;

            //Assert
            result.Value.Should().BeOfType<Category>();
            result.Value.Should().BeEquivalentTo(category);
        }

        [Fact]
        public void GetCategory_WhenCalled_ReturnNotFound()
        {
            //Arrange
            _categoryService.Setup(repo => repo.GetCategory(It.IsAny<long>()).Result).Returns((Category)null);

            //Act
            var response = CategoriesEndpoints.GetCategory(1, _categoryService.Object).Result;
            var result = response as NotFound;

            //Assert
            result.Should().BeOfType<NotFound>();
        }

        [Fact]
        public void GetCategory_WhenException_ReturnProblem()
        {
            //Arrange
            _categoryService.Setup(rep => rep.GetCategory(It.IsAny<long>()).Result)
                .Throws(new Exception("Test Exception"));

            //Act
            var response = CategoriesEndpoints.GetCategory(1, _categoryService.Object).Result;
            var result = response as ProblemHttpResult;

            //Assert
            result.Should().BeOfType<ProblemHttpResult>();
            result.Should().Match<ProblemHttpResult>(r => r.StatusCode == StatusCodes.Status500InternalServerError
                                                  && r.ProblemDetails.Detail == "Test Exception");
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
            var response = CategoriesEndpoints.GetCategoryProducts(1, _categoryService.Object).Result;
            var result = response as Ok<IEnumerable<Product>>;

            //Assert
            result.Should().BeOfType<Ok<IEnumerable<Product>>>();
            result.Value.Should().BeOfType<List<Product>>();
            result.Value.Should().BeEquivalentTo(products);
        }

        [Fact]
        public void GetCategoryProducts_WhenCalled_ReturnNotFound()
        {
            //Arrange
            _categoryService.Setup(repo => repo.GetCategoryProducts(It.IsAny<long>()).Result).Returns(Enumerable.Empty<Product>());

            //Act
            var response = CategoriesEndpoints.GetCategoryProducts(1, _categoryService.Object).Result;
            var result = response as NotFound;

            //Assert
            result.Should().BeOfType<NotFound>();
        }

        [Fact]
        public void GetCategoryProducts_WhenException_ReturnProblem()
        {
            //Arrange
            _categoryService.Setup(rep => rep.GetCategoryProducts(It.IsAny<long>()).Result)
                .Throws(new Exception("Test Exception"));

            //Act
            var response = CategoriesEndpoints.GetCategoryProducts(1, _categoryService.Object).Result;
            var result = response as ProblemHttpResult;

            //Assert
            result.Should().BeOfType<ProblemHttpResult>();
            result.Should().Match<ProblemHttpResult>(r => r.StatusCode == StatusCodes.Status500InternalServerError
                                                  && r.ProblemDetails.Detail == "Test Exception");
        }

        #endregion


        #region CreateCategory

        [Fact]
        public void CreateCategory_WhenCalled_ReturnOk()
        {
            //Arrange
            CategoryInsertDto newCategory = new()
            {
                Name = "Corner Flags",
            };

            _categoryService.Setup(rep => rep.CreateCategory(It.IsAny<Category>()).Result).Returns(1);

            //Act
            var response = CategoriesEndpoints.CreateCategory(newCategory, _categoryService.Object, _mapper).Result;
            var result = response as Ok;

            //Assert
            result.Should().BeOfType<Ok>();
        }

        [Fact]
        public void CreateCategory_WhenCalled_ReturnBadRequest()
        {
            //Arrange
            CategoryInsertDto newCategory = new()
            {
                Name = "Corner Flags",
            };
            _categoryService.Setup(rep => rep.CreateCategory(It.IsAny<Category>()).Result).Returns(0);



            //Act
            var response = CategoriesEndpoints.CreateCategory(newCategory, _categoryService.Object, _mapper).Result;
            var result = response as BadRequest;

            //Assert
            result.Should().BeOfType<BadRequest>();
        }

        [Fact]
        public void CreateCategory_WhenException_ReturnProblem()
        {
            //Arrange
            CategoryInsertDto newCategory = new()
            {
                Name = "Corner Flags",
            };
            _categoryService.Setup(rep => rep.CreateCategory(It.IsAny<Category>()).Result)
                .Throws(new Exception("Test Exception"));

            //Act
            var response = CategoriesEndpoints.CreateCategory(newCategory, _categoryService.Object, _mapper).Result;
            var result = response as ProblemHttpResult;

            //Assert
            result.Should().BeOfType<ProblemHttpResult>();
            result.Should().Match<ProblemHttpResult>(r => r.StatusCode == StatusCodes.Status500InternalServerError
                                                  && r.ProblemDetails.Detail == "Test Exception");
        }

        #endregion


        #region UpdateCategory

        [Fact]
        public void UpdateCategory_WhenCalled_ReturnOk()
        {
            //Arrange
            CategoryUpdateDto newCategory = new()
            {
                Id = 2,
                Name = "Corner Flags",
            };
            _categoryService.Setup(rep => rep.UpdateCategory(It.IsAny<Category>()).Result).Returns(1);

            //Act
            var response = CategoriesEndpoints.UpdateCategory(newCategory, _categoryService.Object, _mapper).Result;
            var result = response as Ok;

            //Assert
            result.Should().BeOfType<Ok>();
        }

        [Fact]
        public void UpdateCategory_WhenCalled_ReturnNotFound()
        {
            //Arrange
            CategoryUpdateDto newCategory = new()
            {
                Id = 2,
                Name = "Corner Flags",
            };
            _categoryService.Setup(rep => rep.UpdateCategory(It.IsAny<Category>()).Result).Returns(0);


            //Act
            var response = CategoriesEndpoints.UpdateCategory(newCategory, _categoryService.Object, _mapper).Result;
            var result = response as NotFound;

            //Assert
            result.Should().BeOfType<NotFound>();
        }

        [Fact]
        public void UpdateCategory_WhenException_ReturnProblem()
        {
            //Arrange
            CategoryUpdateDto newCategory = new()
            {
                Id = 2,
                Name = "Corner Flags",
            };
            _categoryService.Setup(rep => rep.UpdateCategory(It.IsAny<Category>()).Result)
                .Throws(new Exception("Test Exception"));

            //Act
            var response = CategoriesEndpoints.UpdateCategory(newCategory, _categoryService.Object, _mapper).Result;
            var result = response as ProblemHttpResult;

            //Assert
            result.Should().BeOfType<ProblemHttpResult>();
            result.Should().Match<ProblemHttpResult>(r => r.StatusCode == StatusCodes.Status500InternalServerError
                                                  && r.ProblemDetails.Detail == "Test Exception");
        }

        #endregion


        #region DeleteCategory

        [Fact]
        public void DeleteCategory_WhenCalled_ReturnOk()
        {
            //Arrange
            _categoryService.Setup(rep => rep.DeleteCategory(1).Result).Returns(1);

            //Act
            var response = CategoriesEndpoints.DeleteCategory(1, _categoryService.Object).Result;
            var result = response as Ok;

            //Assert
            result.Should().BeOfType<Ok>();
        }

        [Fact]
        public void DeleteCategory_WhenCalled_ReturnNotFound()
        {
            //Arrange
            _categoryService.Setup(rep => rep.DeleteCategory(0).Result).Returns(0);


            //Act
            var response = CategoriesEndpoints.DeleteCategory(1, _categoryService.Object).Result;
            var result = response as NotFound;

            //Assert
            result.Should().BeOfType<NotFound>();
        }

        [Fact]
        public void DeleteCategory_WhenException_ReturnProblem()
        {
            //Arrange
            _categoryService.Setup(rep => rep.DeleteCategory(It.IsAny<long>()).Result)
                .Throws(new Exception("Test Exception"));

            //Act
            var response = CategoriesEndpoints.DeleteCategory(1, _categoryService.Object).Result;
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
                cfg.AddProfile(new CategoryProfile());
            });

            return mockMapper.CreateMapper();
        }
    }
}
