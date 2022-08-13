using BasketService.Api;
using EventBus.Base.Abstrasctions;
using MediatR;
using OrderService.Api.IntegrationEvents.Events;
using OrderService.Application.ViewModels;

namespace OrderService.Api.IntegrationEvents.EventHandlers
{
    public class OrderCreateIntegrationEventHandler : IIntegrationEventHandler<OrderCreateIntegrationEvent>
    {
        private readonly IMediator mediator;
        private readonly ILogger<OrderCreateIntegrationEventHandler> logger;

        public OrderCreateIntegrationEventHandler(IMediator mediator, ILogger<OrderCreateIntegrationEventHandler> logger)
        {
            this.mediator = mediator;
            this.logger = logger;
        }

        public async Task Handle(OrderCreateIntegrationEvent @event)
        {
            logger.LogWarning($"handler integrationt event: {@event.ID} at {typeof(Startup).Namespace},{@event}");

            var createOrderCommand = new CreateOrderCommand(@event.Basket.items,@event.UserId,@event.UserName,@event.City,@event.Street,
                @event.State,@event.Contry,@event.ZipCode,@event.CartNumber,@event.CartHoldName,@event.CartExpresion,@event.CartSecurityNumber,@event.CartTypeId);
            await mediator.Send(createOrderCommand);
        }
    }
}
