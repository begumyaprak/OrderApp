using MediatR;

namespace OrderService.Application.Commands;

public class UpdateOrderStatusCommand : IRequest<bool>
{
    public Guid OrderId { get; set; }
    
    public string NewStatus { get; set; }
    
}