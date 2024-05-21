using Xunit;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using OrderService.Application.Commands;
using OrderService.Application.Queries;
using OrderService.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderService.Api.Controllers;

public class OrdersControllerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly OrdersController _controller;

    public OrdersControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _controller = new OrdersController(_mediatorMock.Object);
    }

    [Fact]
    public async Task GetOrderById_ReturnsNotFound_WhenOrderDoesNotExist()
    {
        var id = Guid.NewGuid();

        _mediatorMock.Setup(m => m.Send(It.Is<GetOrderByIdQuery>(q => q.OrderId == id), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Order)null);

        var result = await _controller.GetOrderById(id);

        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task CreateOrder_ReturnsBadRequest_WhenCreationFails()
    {
        _mediatorMock.Setup(m => m.Send(It.IsAny<CreateOrderCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Guid.Empty);

        var result = await _controller.CreateOrder(new CreateOrderCommand());

        Assert.IsType<BadRequestObjectResult>(result.Result);
    }

    [Fact]
    public async Task UpdateOrder_ReturnsNotFound_WhenUpdateFails()
    {
        var id = Guid.NewGuid();

        _mediatorMock.Setup(m => m.Send(It.Is<UpdateOrderCommand>(cmd => cmd.OrderId == id), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        var result = await _controller.UpdateOrder(id, new UpdateOrderCommand { OrderId = id });

        Assert.IsType<NotFoundResult>(result);
    }
    
    
    [Fact]
    public async Task DeleteOrder_ReturnsNotFound_WhenDeleteFails()
    {
        _mediatorMock.Setup(m => m.Send(It.Is<DeleteOrderCommand>(cmd => cmd.OrderId == It.IsAny<Guid>()), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        var result = await _controller.DeleteOrder(Guid.NewGuid());

        Assert.IsType<NotFoundResult>(result);
    }
    
    [Fact]
    public async Task UpdateOrderStatus_ReturnsBadRequest_WhenIdMismatch()
    {
        var id = Guid.NewGuid();
        var command = new UpdateOrderStatusCommand { OrderId = Guid.NewGuid() };

        _mediatorMock.Setup(m => m.Send(It.IsAny<UpdateOrderStatusCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        var result = await _controller.UpdateOrderStatus(id, command);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task DeleteOrder_ReturnsOk_WhenDeleteSucceeds()
    {
        var id = Guid.NewGuid();

        _mediatorMock.Setup(m => m.Send(It.Is<DeleteOrderCommand>(cmd => cmd.OrderId == id), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        var result = await _controller.DeleteOrder(id);

        Assert.IsType<OkObjectResult>(result);
    }
}