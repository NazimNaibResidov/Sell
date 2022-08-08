using MediatR;
using OrderService.Application.ViewModels;
using System;

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