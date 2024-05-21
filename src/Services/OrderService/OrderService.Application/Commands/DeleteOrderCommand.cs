﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.Commands
{
    public class DeleteOrderCommand : IRequest<bool>
    {
        public Guid OrderId { get; set; }
    }
}
