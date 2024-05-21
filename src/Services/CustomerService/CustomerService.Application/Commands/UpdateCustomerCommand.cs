using CustomerService.Domain.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerService.Application.Commands
{
    public class UpdateCustomerCommand : IRequest<bool>
    {
        public Guid CustomerId { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public Address? Address { get; set; }
    }
}
 