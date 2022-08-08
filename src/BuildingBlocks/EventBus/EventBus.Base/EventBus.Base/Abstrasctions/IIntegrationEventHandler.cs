using EventBus.Base.Events;

namespace EventBus.Base.Abstrasctions
{
    public interface IIntegrationEventHandler<T> : IntegrationEventHandler where T : IntegrationEvent
    {
        Task Handle(T @event);
    }
}