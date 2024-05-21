using CustomerService.Application.Commands;
using CustomerService.Application.Queries;
using CustomerService.Application.Validators;
using CustomerService.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CustomerService.Api.Controllers
{
    [ApiController]
    [Route("api/customers")]
    public class CustomersController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CustomersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomerById(Guid id)
        {
            
            var query = new GetCustomerByIdQuery { CustomerId = id };
            var customer = await _mediator.Send(query);
            if (customer != null)
                return Ok(customer);
            else
                return NotFound();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetAllCustomers()
        {
            var query = new ListAllCustomersQuery();
            var customers = await _mediator.Send(query);
            return Ok(customers);
        }


        [HttpPost]
        public async Task<ActionResult<Guid>> CreateCustomer([FromBody] CreateCustomerCommand command)
        {
            var customerId = await _mediator.Send(command);
            return Ok(customerId);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCustomer(Guid id, [FromBody] UpdateCustomerCommand command)
        {   
            if (id != command.CustomerId)
            {
                return BadRequest("ID mismatch");
            }

            var result = await _mediator.Send(command);
            if (result)
                return Ok("Successfully updated customer.");
            else
                return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCustomer(Guid id)
        {
            var command = new DeleteCustomerCommand { CustomerId = id };
            var result = await _mediator.Send(command);
            if (result)
                return Ok("Successfully deleted customer.");
            else
                return NotFound();
        }
        
        [HttpPost("validate-customer/{id}")]
        public async Task<IActionResult> ValidateCustomer(Guid id)
        {
            var command = new ValidateCustomerCommand(id);
            var isValid = await _mediator.Send(command);
            if (!isValid)
            {
                return BadRequest("invalid customer data");
            }

            return Ok("customer data valid.");
        }
    }
}
