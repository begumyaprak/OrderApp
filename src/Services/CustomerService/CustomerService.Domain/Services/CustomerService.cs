using CustomerService.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerService.Domain.Services
{
    public class CustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<bool> ValidateCustomer(Guid customerId)
        {
            var customer = await _customerRepository.GetByIdAsync(customerId);
            return customer != null;
        }
    }
}
