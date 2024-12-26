using FluentValidation;
using FluentOrdersValidation.Models;

namespace FluentOrdersValidation.Validators
{
    public class CustomerValidator : AbstractValidator<Customer>
    {
        private readonly List<string> _allowedEmailDomains = new() { "example.com", "mycompany.com" };

        public CustomerValidator()
        {
            RuleFor(c => c.FirstName)
                .NotEmpty()
                .WithMessage("First name cannot be empty.");

            RuleFor(c => c.LastName)
                .NotEmpty()
                .WithMessage("Last name cannot be empty.");

            RuleFor(c => c.Email)
                .NotEmpty()
                .WithMessage("Email cannot be empty.")
                .EmailAddress()
                .WithMessage("Invalid email format.")
                .Must(HasAllowedEmailDomain)
                .WithMessage("Email domain is not allowed.");

            RuleFor(c => c.Order)
                .NotNull()
                .WithMessage("Customer must have an order.")
                .SetValidator(new OrderValidator());
        }

        private bool HasAllowedEmailDomain(string email)
        {
            var domain = email.Split('@')[^1];
            return _allowedEmailDomains.Contains(domain);
        }
    }
}