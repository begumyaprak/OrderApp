using MediatR;
using OrderService.Domain.Entities;
using OrderService.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.Commands
{
    public class UpdateOrderCommand : IRequest<bool>
    {
        public Guid OrderId { get; set; }
        public int? Quantity { get; set; }
        public double? Price { get; set; }
        public Address? Address { get; set; }
        
        public Guid? ProductId { get; set; }
    }
}
