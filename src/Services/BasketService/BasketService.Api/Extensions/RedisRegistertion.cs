using BasketService.Api.IntegrationEvents.EventHandler;
using BasketService.Api.IntegrationEvents.Events;
using EventBus.Base.Abstrasctions;
using StackExchange.Redis;

namespace BasketService.Api.Extensions
{
    public static class RedisRegistertion
    {
        public static ConnectionMultiplexer ConfigurRedis(this IServiceProvider servic, IConfiguration confguration)
        {
            var redisConfig = ConfigurationOptions.Parse(confguration["Redis:connectionString"], true);
            redisConfig.ResolveDns = true;
            return ConnectionMultiplexer.Connect(redisConfig);
        }

        private static void LoadSubscribe(this IServiceProvider servic)
        {
            var eventBus = servic.GetRequiredService<IEventBus>();
            eventBus.Subscribe<OrderCreatedIntegrationEvents, OrderCreatedIntegrationEventsHandler>();
        }
    }
}