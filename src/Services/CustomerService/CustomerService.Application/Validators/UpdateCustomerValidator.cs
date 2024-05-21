using CustomerService.Application.Commands;
using FluentValidation;

namespace CustomerService.Application.Validators;

public class UpdateCustomerValidator : AbstractValidator<UpdateCustomerCommand>
{
    public UpdateCustomerValidator()
    {
        RuleFor(x => x.CustomerId)
            .NotEmpty()
            .WithMessage("Customer ID is required.");

        When(x => x.Name != null, () =>
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name is required.")
                .Length(1, 100)
                .WithMessage("Name must be between 1 and 100 characters.");
        });

        When(x => x.Email != null, () =>
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email is required.")
                .EmailAddress()
                .WithMessage("Email must be a valid email address.");
        });

        When(x => x.Address != null, () =>
        {
            RuleFor(x => x.Address)
                .SetValidator(new AddressValidator());
        });
    }
}