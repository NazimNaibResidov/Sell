using EventBus.Base.Events;

namespace OrderService.Api.IntegrationEvents.Events
{
    public class OrderStartedIntegrationEvent : IntegrationEvent
    {
        public OrderStartedIntegrationEvent(string userId, string orderId)
        {
            UserId = userId;
            OrderId = orderId;
        }

        public string UserId { get; set; }
        public string OrderId { get; set; }
    }
}
