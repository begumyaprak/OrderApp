using CustomerService.Application.CommandHandlers;
using CustomerService.Application.Commands;
using CustomerService.Domain.Entities;
using CustomerService.Domain.Interfaces;
using Moq;
using Xunit;

namespace CustomerService.Application.Test;

public class CreateCustomerCommandHandlerTests
{
    private readonly Mock<ICustomerRepository> _customerRepositoryMock;
    private readonly CreateCustomerCommandHandler _handler;

    public CreateCustomerCommandHandlerTests()
    {
        _customerRepositoryMock = new Mock<ICustomerRepository>();
        _handler = new CreateCustomerCommandHandler(_customerRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ReturnsCustomerId_WhenCustomerIsCreatedSuccessfully()
    {
        var command = new CreateCustomerCommand();
        var customer = new Customer();

        _customerRepositoryMock.Setup(m => m.AddAsync(It.IsAny<Customer>())).ReturnsAsync(customer.Id);

        var result = await _handler.Handle(command, CancellationToken.None);

        Assert.Equal(customer.Id, result);
    }
    
}