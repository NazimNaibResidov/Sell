using MediatR;
using OrderService.Domain.AggregateModel.OrderAggreage;

namespace OrderService.Domain.Events
{
    public class OrderStatusChangedToAwaitingValidationDomainEvent
        : INotification
    {
        public int OrderId { get; }
        public IEnumerable<OrderItem> OrderItems { get; }

        public OrderStatusChangedToAwaitingValidationDomainEvent(int orderId,
            IEnumerable<OrderItem> orderItems)
        {
            this.OrderId = orderId;
            OrderItems = orderItems;
        }
    }
}