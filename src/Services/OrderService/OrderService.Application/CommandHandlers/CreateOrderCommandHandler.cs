using MediatR;
using OrderService.Application.Commands;
using OrderService.Domain.Entities;
using OrderService.Domain.Interfaces;
using OrderService.Application.Queries;

namespace OrderService.Application.CommandHandlers
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Guid>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMediator _mediator;
        private readonly IProductRepository _productRepository;

        public CreateOrderCommandHandler(IOrderRepository orderRepository, IMediator mediator, IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _mediator = mediator;
            _productRepository = productRepository;
        }
        public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            
            var product = await _productRepository.GetByIdAsync(request.ProductId);
            
            if (product == null) throw new Exception("Product not found");
            
            var order = new Order
            {
                CustomerId = request.CustomerId,
                Quantity = request.Quantity,
                Price = request.Price,
                Status = "Created",
                Address = request.Address,
                ProductId = product.Id,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _orderRepository.AddAsync(order);
            return order.Id;
        }
    }
}
