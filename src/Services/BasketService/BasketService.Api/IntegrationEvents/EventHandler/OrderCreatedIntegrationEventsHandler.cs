using BasketService.Api.IntegrationEvents.Events;
using BasketService.Api.Interfaces;
using EventBus.Base.Abstrasctions;

namespace BasketService.Api.IntegrationEvents.EventHandler
{
    public class OrderCreatedIntegrationEventsHandler : IIntegrationEventHandler<OrderCreatedIntegrationEvents>
    {
        private readonly IBasketRepsotory basketRepsotory;
        private readonly ILogger<OrderCreatedIntegrationEventsHandler> logger;

        public OrderCreatedIntegrationEventsHandler(IBasketRepsotory basketRepsotory, ILogger<OrderCreatedIntegrationEventsHandler> logger)
        {
            this.basketRepsotory = basketRepsotory;
            this.logger = logger;
        }

        public async Task Handle(OrderCreatedIntegrationEvents @event)
        {
            logger.LogInformation("Handling eventer event ");
            await basketRepsotory.DeleteBasketAsync(@event.UserId.ToString());
        }
    }
}