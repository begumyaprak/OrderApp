using CustomerService.Domain.ValueObjects;
using FluentValidation;

namespace CustomerService.Application.Validators;

public class AddressValidator : AbstractValidator<Address>
{
    public AddressValidator()
    {
        RuleFor(x => x.AddressLine)
            .NotEmpty()
            .WithMessage("Address line is required.")
            .Length(1, 100)
            .WithMessage("Address line must be between 1 and 100 characters.");

        RuleFor(x => x.City)
            .NotEmpty()
            .WithMessage("City is required.")
            .Length(1, 50)
            .WithMessage("City must be between 1 and 50 characters.");

        RuleFor(x => x.Country)
            .NotEmpty()
            .WithMessage("Country is required.")
            .Length(1, 50)
            .WithMessage("Country must be between 1 and 50 characters.");

        RuleFor(x => x.CityCode)
            .GreaterThan(0)
            .WithMessage("City code must be greater than 0.");
    }
}