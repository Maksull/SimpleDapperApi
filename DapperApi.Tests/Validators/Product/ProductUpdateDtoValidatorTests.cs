using DapperApi.Models.Dto.Product;
using DapperApi.Validators.Product;

namespace DapperApi.Tests.Validators.Product
{
    public sealed class ProductUpdateDtoValidatorTests
    {
        [Fact]
        public void Validate_WhenCalled_ShouldNotHaveErrors_For_ValidProductUpdateDto()
        {
            //Arrange
            ProductUpdateDto productUpdateDto = new()
            {
                Id = 1,
                Name = "First",
                Description = "Description",
                Price = 1,
                CategoryId = 1,
            };
            ProductUpdateDtoValidator validator = new();

            //Act
            var result = validator.Validate(productUpdateDto);

            //Assert
            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Validate_WhenCalled_ShouldHaveErrors_For_InvalidProductUpdateDto()
        {
            //Arrange
            ProductUpdateDto productUpdateDto = new()
            {
                Id = 0,
                Name = "  ",
                Description = "  ",
                Price = -1,
                CategoryId = 0,
            };
            ProductUpdateDtoValidator validator = new();

            //Act
            var result = validator.Validate(productUpdateDto);

            //Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().Be(5);
            result.Errors.Should().Contain(e => e.PropertyName == "Id");
            result.Errors.Should().Contain(e => e.PropertyName == "Name");
            result.Errors.Should().Contain(e => e.PropertyName == "Description");
            result.Errors.Should().Contain(e => e.PropertyName == "Price");
            result.Errors.Should().Contain(e => e.PropertyName == "CategoryId");
        }
    }
}
