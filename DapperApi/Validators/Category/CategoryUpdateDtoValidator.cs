using DapperApi.Models.Dto.Category;
using FluentValidation;

namespace DapperApi.Validators.Category
{
    public class CategoryUpdateDtoValidator : AbstractValidator<CategoryUpdateDto>
    {
        public CategoryUpdateDtoValidator()
        {
            RuleFor(c => c.Id).GreaterThanOrEqualTo(1);
            RuleFor(c => c.Name).NotEmpty();
        }
    }
}
