using MediatR;

namespace CustomerService.Application.Commands;

public class ValidateCustomerCommand : IRequest<bool>
{
    public Guid CustomerId { get; set; }

    public ValidateCustomerCommand(Guid customerId)
    {
        CustomerId = customerId;
    }
}