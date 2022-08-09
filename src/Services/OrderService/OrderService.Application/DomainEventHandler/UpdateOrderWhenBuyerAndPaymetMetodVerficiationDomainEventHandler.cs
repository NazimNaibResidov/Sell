using MediatR;
using OrderService.Domain.Events;

namespace OrderService.Application.DomainEventHandler
{
    public class UpdateOrderWhenBuyerAndPaymetMetodVerficiationDomainEventHandler : INotificationHandler<OrderStartedDomainEvent>
    {
        public Task Handle(OrderStartedDomainEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}