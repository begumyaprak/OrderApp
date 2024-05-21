using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderService.Application.Commands;
using OrderService.Application.Queries;
using OrderService.Domain.Entities;
namespace OrderService.Api.Controllers
{

    [ApiController]
    [Route("api/orders")]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrderById(Guid id)
        {
            var query = new GetOrderByIdQuery { OrderId = id };
            var order = await _mediator.Send(query);
            if (order != null)
                return Ok(order);
            else
                return NotFound();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetAllOrders()
        {
            var query = new ListAllOrdersQuery();
            var orders = await _mediator.Send(query);
            return Ok(orders);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateOrder([FromBody] CreateOrderCommand command)
        {
            var orderId = await _mediator.Send(command);
            if (orderId != Guid.Empty)
            {
                return Ok(orderId);
            }
            else
            {
                return BadRequest("Order creation failed.");
            }
           
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateOrder(Guid id, [FromBody] UpdateOrderCommand command)
        {
            if (id != command.OrderId)
            {
                return BadRequest("ID mismatch");
            }

            var result = await _mediator.Send(command);
            if (result)
                return Ok("Order updated successfully.");
            else
                return NotFound();
        }
        
        [HttpPut("{id}/status")]
        public async Task<ActionResult> UpdateOrderStatus(Guid id, [FromBody] UpdateOrderStatusCommand command)
        {
            if (id != command.OrderId)
            {
                return BadRequest("ID mismatch");
            }

            var result = await _mediator.Send(command);
            if (result)
                return Ok("Order status updated successfully.");
            else
                return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOrder(Guid id)
        {
            var command = new DeleteOrderCommand { OrderId = id };
            var result = await _mediator.Send(command);
            if (result)
                return Ok("Order deleted successfully.");
            else
                return NotFound();
        }
    }


}
