using Moq;
using OrderService.Application.CommandHandlers;
using OrderService.Application.Commands;
using OrderService.Domain.Entities;
using OrderService.Domain.Interfaces;
using Xunit;

namespace OrderService.Application.Test;

public class UpdateOrderCommandHandlerTests
{
    private readonly Mock<IOrderRepository> _orderRepositoryMock;
    private readonly Mock<IProductRepository> _productRepositoryMock;
    private readonly UpdateOrderCommandHandler _handler;

    public UpdateOrderCommandHandlerTests()
    {
        _orderRepositoryMock = new Mock<IOrderRepository>();
        _productRepositoryMock = new Mock<IProductRepository>();
        _handler = new UpdateOrderCommandHandler(_orderRepositoryMock.Object, _productRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ReturnsFalse_WhenOrderNotFound()
    {
        var command = new UpdateOrderCommand { OrderId = Guid.NewGuid() };

        _orderRepositoryMock.Setup(m => m.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Order)null);

        var result = await _handler.Handle(command, CancellationToken.None);

        Assert.False(result);
    }

    [Fact]
    public async Task Handle_ThrowsException_WhenProductNotFound()
    {
        var command = new UpdateOrderCommand { OrderId = Guid.NewGuid(), ProductId = Guid.NewGuid() };
        var order = new Order();

        _orderRepositoryMock.Setup(m => m.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(order);
        _productRepositoryMock.Setup(m => m.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Product)null);

        await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_ReturnsTrue_WhenOrderUpdatedSuccessfully()
    {
        var command = new UpdateOrderCommand { OrderId = Guid.NewGuid(), ProductId = Guid.NewGuid() };
        var order = new Order();
        var product = new Product();

        _orderRepositoryMock.Setup(m => m.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(order);
        _productRepositoryMock.Setup(m => m.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(product);
        _orderRepositoryMock.Setup(m => m.UpdateAsync(It.IsAny<Order>())).ReturnsAsync(true);

        var result = await _handler.Handle(command, CancellationToken.None);

        Assert.True(result);
    }
}
