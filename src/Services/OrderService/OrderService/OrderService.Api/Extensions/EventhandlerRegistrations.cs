using OrderService.Api.IntegrationEvents.EventHandlers;

namespace OrderService.Api.Extensions
{
    public static class EventhandlerRegistrations
    {
        public static IServiceCollection ConfigrationEvents(this IServiceCollection services)
        {
            services.AddTransient<OrderCreateIntegrationEventHandler>();
            return services;
        }
    }
}