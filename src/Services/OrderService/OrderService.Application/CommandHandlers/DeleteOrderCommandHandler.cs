﻿using MediatR;
using OrderService.Application.Commands;
using OrderService.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.CommandHandlers
{
    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, bool>
    {
        private readonly IOrderRepository _orderRepository;

        public DeleteOrderCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<bool> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            return await _orderRepository.DeleteAsync(request.OrderId);
        }

    }
}
