using FluentValidation;
using FluentOrdersValidation.Models;

namespace FluentOrdersValidation.Validators
{
    public class OrderValidator : AbstractValidator<Order>
    {
        public OrderValidator()
        {
            RuleFor(o => o.OrderDate)
                .NotEmpty()
                .WithMessage("Order date cannot be empty.")
                .LessThanOrEqualTo(DateTime.Now)
                .WithMessage("Order date cannot be in the future.");

            RuleFor(o => o.Products)
                .NotEmpty()
                .WithMessage("Order must have at least one product.")
                .ForEach(product =>
                {
                    product.SetValidator(new ProductValidator());
                });
        }
    }
}