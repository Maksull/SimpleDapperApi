using DapperApi.Models.Dto.Category;
using DapperApi.Validators.Category;

namespace DapperApi.Tests.Validators.Category
{
    public sealed class CategoryInsertDtoValidatorTests
    {
        [Fact]
        public void Validate_WhenCalled_ShouldNotHaveErrors_For_ValidCategoryInsertDto()
        {
            //Arrange
            CategoryInsertDto categoryInsertDto = new()
            {
                Name = "First",
            };
            CategoryInsertDtoValidator validator = new();

            //Act
            var result = validator.Validate(categoryInsertDto);

            //Assert
            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Validate_WhenCalled_ShouldHaveErrors_For_InvalidCategoryInsertDto()
        {
            //Arrange
            CategoryInsertDto categoryInsertDto = new()
            {
                Name = "  ",
            };
            CategoryInsertDtoValidator validator = new();

            //Act
            var result = validator.Validate(categoryInsertDto);

            //Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().Be(1);
            result.Errors.Should().Contain(e => e.PropertyName == "Name");
        }
    }
}
