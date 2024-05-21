using FluentValidation;
using OrderService.Application.Commands;

namespace OrderService.Application.Validators;

public class UpdateOrderStatusValidator : AbstractValidator<UpdateOrderStatusCommand>
{
    public UpdateOrderStatusValidator()
    {
        RuleFor(command => command.NewStatus)
            .NotEmpty().WithMessage("New status is required")
            .MaximumLength(25).WithMessage("Status must not exceed 25 characters");
    }
}