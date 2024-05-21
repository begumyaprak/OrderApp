using CustomerService.Application.Queries;
using CustomerService.Domain.Entities;
using CustomerService.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerService.Application.QueryHandlers
{
    public class GetCustomerByIdQueryHandler :IRequestHandler<GetCustomerByIdQuery,Customer>
    {
        private readonly ICustomerRepository _customerRepository;

        public GetCustomerByIdQueryHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<Customer> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            return await _customerRepository.GetByIdAsync(request.CustomerId);
        }

    }
}
