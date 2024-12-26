using FluentValidation;
using FluentOrdersValidation.Models;

namespace FluentOrdersValidation.Validators
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .WithMessage("Product name cannot be empty.");

            RuleFor(p => p.Price)
                .GreaterThan(0)
                .WithMessage("Product price must be greater than zero.");
        }
    }
}