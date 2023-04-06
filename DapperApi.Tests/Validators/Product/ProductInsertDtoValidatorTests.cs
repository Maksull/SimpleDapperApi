using DapperApi.Models.Dto.Product;
using DapperApi.Validators.Product;

namespace DapperApi.Tests.Validators.Product
{
    public sealed class ProductInsertDtoValidatorTests
    {
        [Fact]
        public void Validate_WhenCalled_ShouldNotHaveErrors_For_ValidProductInsertDto()
        {
            //Arrange
            ProductInsertDto productInsertDto = new()
            {
                Name = "First",
                Description = "Description",
                Price = 1,
                CategoryId = 1,
            };
            ProductInsertDtoValidator validator = new();

            //Act
            var result = validator.Validate(productInsertDto);

            //Assert
            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Validate_WhenCalled_ShouldHaveErrors_For_InvalidProductUpdateDto()
        {
            //Arrange
            ProductInsertDto productInsertDto = new()
            {
                Name = "  ",
                Description = "  ",
                Price = -1,
                CategoryId = 0,
            };
            ProductInsertDtoValidator validator = new();

            //Act
            var result = validator.Validate(productInsertDto);

            //Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().Be(4);
            result.Errors.Should().Contain(e => e.PropertyName == "Name");
            result.Errors.Should().Contain(e => e.PropertyName == "Description");
            result.Errors.Should().Contain(e => e.PropertyName == "Price");
            result.Errors.Should().Contain(e => e.PropertyName == "CategoryId");
        }
    }
}
