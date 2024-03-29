﻿using MediatR;
using OrderService.Application.ViewModels;

namespace OrderService.Application.Features.Queries.GetOrderbyId
{
    public class GetOrderDetailsQuery : IRequest<OrderDatialViewModel>
    {
        public GetOrderDetailsQuery(Guid orderId)
        {
            OrderId = orderId;
        }

        public Guid OrderId { get; set; }
    }
}