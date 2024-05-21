using Moq;
using CustomerService.Application.CommandHandlers;
using CustomerService.Application.Commands;
using CustomerService.Domain.Entities;
using CustomerService.Domain.Interfaces;
using Xunit;

namespace CustomerService.Application.Test;

public class UpdateCustomerCommandHandlerTests
{
    private readonly Mock<ICustomerRepository> _customerRepositoryMock;
    private readonly UpdateCustomerCommandHandler _handler;

    public UpdateCustomerCommandHandlerTests()
    {
        _customerRepositoryMock = new Mock<ICustomerRepository>();
        _handler = new UpdateCustomerCommandHandler(_customerRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ReturnsFalse_WhenCustomerNotFound()
    {
        var command = new UpdateCustomerCommand { CustomerId = Guid.NewGuid() };

        _customerRepositoryMock.Setup(m => m.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Customer)null);

        var result = await _handler.Handle(command, CancellationToken.None);

        Assert.False(result);
    }

    [Fact]
    public async Task Handle_ReturnsTrue_WhenCustomerUpdatedSuccessfully()
    {
        var command = new UpdateCustomerCommand { CustomerId = Guid.NewGuid(), Name = "New Name" };
        var customer = new Customer();

        _customerRepositoryMock.Setup(m => m.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(customer);
        _customerRepositoryMock.Setup(m => m.UpdateAsync(It.IsAny<Customer>())).ReturnsAsync(true);

        var result = await _handler.Handle(command, CancellationToken.None);

        Assert.True(result);
    }
}