using CustomerService.Application.Commands;
using CustomerService.Domain.Entities;
using CustomerService.Domain.Interfaces;
using MediatR;

namespace CustomerService.Application.CommandHandlers
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand , Guid>
    {
        private readonly ICustomerRepository _customerRepository;
        public CreateCustomerCommandHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<Guid> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = new Customer
            {
                Name = request.Name,
                Email = request.Email,
                Address = request.Address,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            await _customerRepository.AddAsync(customer);
            return customer.Id;
        }
    }
}
