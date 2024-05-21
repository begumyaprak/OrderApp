using CustomerService.Application.Queries;
using CustomerService.Domain.Entities;
using CustomerService.Domain.Interfaces;
using MediatR;

namespace CustomerService.Application.QueryHandlers
{
    public class ListAllCustomersQueryHandler : IRequestHandler<ListAllCustomersQuery,IEnumerable<Customer>>
    {
        private readonly ICustomerRepository _customerRepository;

        public ListAllCustomersQueryHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<IEnumerable<Customer>> Handle(ListAllCustomersQuery request, CancellationToken cancellationToken)
        {
            return await _customerRepository.GetAllAsync();
        }
    }
}
