using MediatR;
using Moq;
using OrderService.Application.CommandHandlers;
using OrderService.Application.Commands;
using OrderService.Domain.Entities;
using OrderService.Domain.Interfaces;
using Xunit;

public class CreateOrderCommandHandlerTests
{
    private readonly Mock<IOrderRepository> _orderRepositoryMock;
    private readonly Mock<IProductRepository> _productRepositoryMock;
    private readonly Mock<IMediator> _mediatorMock;
    private readonly CreateOrderCommandHandler _handler;

    public CreateOrderCommandHandlerTests()
    {
        _orderRepositoryMock = new Mock<IOrderRepository>();
        _productRepositoryMock = new Mock<IProductRepository>();
        _mediatorMock = new Mock<IMediator>();
        _handler = new CreateOrderCommandHandler(_orderRepositoryMock.Object, _mediatorMock.Object, _productRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ReturnsOrderId_WhenOrderIsCreatedSuccessfully()
    {
        var command = new CreateOrderCommand();
        var product = new Product();
        var order = new Order();

        _productRepositoryMock.Setup(m => m.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(product);
        _orderRepositoryMock.Setup(m => m.AddAsync(It.IsAny<Order>())).ReturnsAsync(order.Id);

        var result = await _handler.Handle(command, CancellationToken.None);

        Assert.Equal(order.Id, result);
    }

    [Fact]
    public async Task Handle_ThrowsException_WhenProductNotFound()
    {
        var command = new CreateOrderCommand();

        _productRepositoryMock.Setup(m => m.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Product)null);

        await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));
    }
}