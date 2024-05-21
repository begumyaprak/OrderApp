using FluentValidation;
using OrderService.Application.Commands;
using OrderService.Application.Validators;

namespace CustomerService.Application.Validators;

public class CreateOrderValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderValidator()
    {
        RuleFor(command => command.Quantity)
            .GreaterThan(0).WithMessage("Quantity must be greater than 0.");
        
        RuleFor(command => command.Price)
            .GreaterThan(0.0).WithMessage("Price must be greater than 0.");
        
        RuleFor(command => command.Address)
            .NotNull().WithMessage("Address is required.")
            .SetValidator(new AddressValidator()); 
     
    }
    
}