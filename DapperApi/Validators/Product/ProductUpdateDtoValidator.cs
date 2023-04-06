using DapperApi.Models.Dto.Product;
using FluentValidation;

namespace DapperApi.Validators.Product
{
    public sealed class ProductUpdateDtoValidator : AbstractValidator<ProductUpdateDto>
    {
        public ProductUpdateDtoValidator()
        {
            RuleFor(p => p.Id).GreaterThanOrEqualTo(1);
            RuleFor(p => p.Name).NotEmpty();
            RuleFor(p => p.Description).NotEmpty();
            RuleFor(p => p.Price).GreaterThan(0);
            RuleFor(p => p.CategoryId).GreaterThanOrEqualTo(1);
        }
    }
}
