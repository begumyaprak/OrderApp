using CustomerService.Application.Commands;
using CustomerService.Domain.Interfaces;
using MediatR;

namespace CustomerService.Application.CommandHandlers;

public class ValidateCustomerCommandHandler : IRequestHandler<ValidateCustomerCommand, bool>
{

    private readonly ICustomerRepository _customerRepository;
    
    public ValidateCustomerCommandHandler(ICustomerRepository customerRepository)
    {   
        _customerRepository = customerRepository;
        
    }
    
    public async Task<bool> Handle(ValidateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByIdAsync(request.CustomerId);

        if (customer == null)
        {
            return false; 
        }

        if (string.IsNullOrWhiteSpace(customer.Name) || !customer.Email.Contains("@"))
        {
            return false; 
        }
        
        return true; //validated 
    }
    
}