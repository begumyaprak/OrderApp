using Xunit;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CustomerService.Api.Controllers;
using CustomerService.Application.Commands;
using CustomerService.Application.Queries;
using CustomerService.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

public class CustomersControllerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly CustomersController _controller;

    public CustomersControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _controller = new CustomersController(_mediatorMock.Object);
    }

    [Fact]
    public async Task GetCustomerById_ReturnsNotFound_WhenCustomerDoesNotExist()
    {
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetCustomerByIdQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Customer)null);

        var result = await _controller.GetCustomerById(Guid.NewGuid());

        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task GetCustomerById_ReturnsCustomer_WhenCustomerExists()
    {
        var expectedCustomer = new Customer();
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetCustomerByIdQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedCustomer);

        var result = await _controller.GetCustomerById(Guid.NewGuid());

        Assert.IsType<OkObjectResult>(result.Result);
        Assert.Equal(expectedCustomer, ((OkObjectResult)result.Result).Value);
    }

    [Fact]
    public async Task GetAllCustomers_ReturnsAllCustomers()
    {
        var expectedCustomers = new List<Customer> { new Customer(), new Customer() };
        _mediatorMock.Setup(m => m.Send(It.IsAny<ListAllCustomersQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedCustomers);

        var result = await _controller.GetAllCustomers();

        Assert.IsType<OkObjectResult>(result.Result);
        Assert.Equal(expectedCustomers, ((OkObjectResult)result.Result).Value);
    }

    [Fact]
    public async Task CreateCustomer_ReturnsCustomerId()
    {
        var expectedId = Guid.NewGuid();
        _mediatorMock.Setup(m => m.Send(It.IsAny<CreateCustomerCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedId);

        var result = await _controller.CreateCustomer(new CreateCustomerCommand());

        Assert.IsType<OkObjectResult>(result.Result);
        Assert.Equal(expectedId, ((OkObjectResult)result.Result).Value);
    }

    [Fact]
    public async Task UpdateCustomer_ReturnsNotFound_WhenUpdateFails()
    {
        
        var id = Guid.NewGuid();
        
        _mediatorMock.Setup(m => m.Send(It.Is<UpdateCustomerCommand>(cmd => cmd.CustomerId == id), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        var result = await _controller.UpdateCustomer(id, new UpdateCustomerCommand { CustomerId = id });

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task UpdateCustomer_ReturnsOk_WhenUpdateSucceeds()
    {
        var id = Guid.NewGuid();
        
        _mediatorMock.Setup(m => m.Send(It.IsAny<UpdateCustomerCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        var result = await _controller.UpdateCustomer(id, new UpdateCustomerCommand{ CustomerId = id });

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DeleteCustomer_ReturnsNotFound_WhenDeleteFails()
    {
        _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteCustomerCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        var result = await _controller.DeleteCustomer(Guid.NewGuid());

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task DeleteCustomer_ReturnsOk_WhenDeleteSucceeds()
    {
        _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteCustomerCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        var result = await _controller.DeleteCustomer(Guid.NewGuid());

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task ValidateCustomer_ReturnsBadRequest_WhenCustomerIsInvalid()
    {
        _mediatorMock.Setup(m => m.Send(It.IsAny<ValidateCustomerCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        var result = await _controller.ValidateCustomer(Guid.NewGuid());

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task ValidateCustomer_ReturnsOk_WhenCustomerIsValid()
    {
        _mediatorMock.Setup(m => m.Send(It.IsAny<ValidateCustomerCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        var result = await _controller.ValidateCustomer(Guid.NewGuid());

        Assert.IsType<OkObjectResult>(result);
    }
}