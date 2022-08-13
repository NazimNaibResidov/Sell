using EventBus.Base.Abstrasctions;
using OrderService.Api.IntegrationEvents.Events;
using OrderService.Application.ViewModels;

namespace OrderService.Api.IntegrationEvents.EventHandlers
{
    public class OrderCreateIntegrationEventHandler : IIntegrationEventHandler<OrderCreateIntegrationEvent>
    {
        public Task Handle(OrderCreateIntegrationEvent @event)
        {
            var createOrderCommand = new CreateOrderCommand(@event.Basket.items,@event.UserId,@event.UserName,@event.City,@event.Street,
                @event.State,@event.Contry,@event.ZipCode,@event.CartNumber,@event.CartHoldName,@event.CartExpresion,@event.CartSecurityNumber,@event.CartTypeId);
        }
    }
}
