using DapperApi.Models.Dto.Category;
using DapperApi.Validators.Category;

namespace DapperApi.Tests.Validators.Category
{
    public sealed class CategoryUpdateDtoValidatorTests
    {
        [Fact]
        public void Validate_WhenCalled_ShouldNotHaveErrors_For_ValidCategoryUpdateDto()
        {
            //Arrange
            CategoryUpdateDto categoryUpdateDto = new()
            {
                Id = 1,
                Name = "First",
            };
            CategoryUpdateDtoValidator validator = new();

            //Act
            var result = validator.Validate(categoryUpdateDto);

            //Assert
            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Validate_WhenCalled_ShouldHaveErrors_For_InvalidCategoryUpdateDto()
        {
            //Arrange
            CategoryUpdateDto categoryUpdateDto = new()
            {
                Id = 0,
                Name = "  ",
            };
            CategoryUpdateDtoValidator validator = new();

            //Act
            var result = validator.Validate(categoryUpdateDto);

            //Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().Be(2);
            result.Errors.Should().Contain(e => e.PropertyName == "Id");
            result.Errors.Should().Contain(e => e.PropertyName == "Name");
        }
    }
}
