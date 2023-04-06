using DapperApi.Models.Dto.Category;
using FluentValidation;

namespace DapperApi.Validators.Category
{
    public sealed class CategoryInsertDtoValidator : AbstractValidator<CategoryInsertDto>
    {
        public CategoryInsertDtoValidator()
        {
            RuleFor(c => c.Name).NotEmpty();
        }
    }
}
