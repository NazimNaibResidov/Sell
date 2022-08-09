using MediatR;
using OrderService.Application.ViewModels;

namespace OrderService.Application.RequestDTO
{
    public class GetOrderDatialQuery : IRequest<OrderDatialViewModel>
    {
        public GetOrderDatialQuery(Guid orderId)
        {
            OrderId = orderId;
        }

        public Guid OrderId { get; set; }
    }
}