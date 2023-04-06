using DapperApi.Models.Dto.Product;
using FluentValidation;

namespace DapperApi.Validators.Product
{
    public sealed class ProductInsertDtoValidator : AbstractValidator<ProductInsertDto>
    {
        public ProductInsertDtoValidator()
        {
            RuleFor(p => p.Name).NotEmpty();
            RuleFor(p => p.Description).NotEmpty();
            RuleFor(p => p.Price).GreaterThan(0);
            RuleFor(p => p.CategoryId).GreaterThanOrEqualTo(1);
        }
    }
}
