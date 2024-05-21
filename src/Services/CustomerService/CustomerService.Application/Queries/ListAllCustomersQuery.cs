using CustomerService.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerService.Application.Queries
{
    public class ListAllCustomersQuery : IRequest<IEnumerable<Customer>>
    {

    }
}
