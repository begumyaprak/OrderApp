using MediatR;
using OrderService.Application.Commands;
using OrderService.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.CommandHandlers
{
    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, bool>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;

        public UpdateOrderCommandHandler(IOrderRepository orderRepository, IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
        }

        public async Task<bool> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(request.OrderId);
            if (order == null)
            {
                return false;
            }
            
            if(request.ProductId.HasValue) 
            {
                var product = await _productRepository.GetByIdAsync(request.ProductId.Value);

                if (product == null) throw new Exception("Product not found");

                order.ProductId = request.ProductId.Value;
            }

            if(request.Quantity.HasValue) 
            {
                order.Quantity = request.Quantity.Value;
            }

            if(request.Price.HasValue) 
            {
                order.Price = request.Price.Value;
            }

            if(request.Address != null) 
            {
                order.Address = request.Address;
            }
            order.UpdatedAt = DateTime.Now;

            return await _orderRepository.UpdateAsync(order);
        }
    }
}
