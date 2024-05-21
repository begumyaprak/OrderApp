using CustomerService.Application.Commands;
using CustomerService.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerService.Application.CommandHandlers
{
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, bool>
    {
        private readonly ICustomerRepository _customerRepository;

        public UpdateCustomerCommandHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<bool> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetByIdAsync(request.CustomerId);
            if (customer == null)
            {
                return false;
            }

            if(request.Name != null) 
            {
                customer.Name = request.Name;
            }

            if(request.Email != null) 
            {
                customer.Email = request.Email;
            }

            if(request.Address != null) 
            {
                customer.Address = request.Address;
            }

            customer.UpdatedAt = DateTime.Now;

            return await _customerRepository.UpdateAsync(customer);
        }
    }
}
