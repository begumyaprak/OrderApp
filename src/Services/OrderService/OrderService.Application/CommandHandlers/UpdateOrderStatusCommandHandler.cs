using MediatR;
using OrderService.Application.Commands;
using OrderService.Domain.Interfaces;

namespace OrderService.Application.CommandHandlers;

public class UpdateOrderStatusCommandHandler : IRequestHandler<UpdateOrderStatusCommand,bool>
{
    private readonly IOrderRepository _orderRepository;

    public UpdateOrderStatusCommandHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<bool> Handle(UpdateOrderStatusCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(request.OrderId);
        if (order == null)
        {
            return false;
        }

        order.Status = request.NewStatus;
        order.UpdatedAt = DateTime.Now;

        return await _orderRepository.UpdateAsync(order);
    }
    
}