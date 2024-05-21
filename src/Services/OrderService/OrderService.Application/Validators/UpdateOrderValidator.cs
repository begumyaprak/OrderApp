using FluentValidation;
using OrderService.Application.Commands;

namespace OrderService.Application.Validators;

public class UpdateOrderValidator : AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderValidator()
    {
        
        When(command => command.Quantity.HasValue, () =>
        {
            RuleFor(command => command.Quantity.Value)
                .GreaterThan(0).WithMessage("Quantity must be greater than 0.");
        });

        
        When(command => command.Price.HasValue, () =>
        {
            RuleFor(command => command.Price.Value)
                .GreaterThan(0.0).WithMessage("Price must be greater than 0.");
        });

     
        When(command => command.Address != null, () =>
        {
            RuleFor(command => command.Address)
                .SetValidator(new AddressValidator());
        });
    }
}