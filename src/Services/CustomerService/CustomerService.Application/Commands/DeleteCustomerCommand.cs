﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerService.Application.Commands
{
    public class DeleteCustomerCommand : IRequest<bool>
    {
        public Guid CustomerId { get; set; }
    }
} 
