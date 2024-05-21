using OrderService.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.Entities

{
    public class Order
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get;  set; }
        public Address Address { get;  set; }
        public Product Product { get; set; }
        
        public Guid ProductId { get; set; }
        public int Quantity { get;  set; }
        public double Price { get;  set; }
        public string Status { get;  set; }
        public DateTime CreatedAt { get;  set; }
        public DateTime UpdatedAt { get;  set; }

        public void UpdateStatus(string newStatus)
        {
            Status = newStatus;
        }
    }
}
