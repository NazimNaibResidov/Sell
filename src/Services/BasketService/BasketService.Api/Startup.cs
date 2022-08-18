using BasketService.Api.Core.Application.Service;
using BasketService.Api.Extensions;
using BasketService.Api.Infrastrucutre.Repostorye;
using BasketService.Api.IntegrationEvents.EventHandler;
using BasketService.Api.IntegrationEvents.Events;
using BasketService.Api.Interfaces;
using EventBus.Base;
using EventBus.Base.Abstrasctions;
using EventBus.Factory;
using Microsoft.OpenApi.Models;

namespace BasketService.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configuration(Configuration);
            ConfigureServiceExit(services);
            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BasketService.Api", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime lifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BasketService.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.RegisterWithConsule(lifetime);
            ConfigureSubscription(app.ApplicationServices);
        }

        private void ConfigureServiceExit(IServiceCollection services)
        {
            services.RegisterionAuth(Configuration);
            services.AddSingleton(sp => sp.ConfigurRedis(Configuration));
            services.AddHttpContextAccessor();
            services.AddLogging()
            services.AddScoped<IBasketRepsotory, BasketRepsotory>();
            services.AddTransient<IIdentityService, IdentityService>();
            services.RegistrationConsule(Configuration);
            services.AddSingleton<IEventBus>(sp =>
            {
                //var correlationSevice = sp.GetRequiredService<ICorrelationService>();
                EventBusConfig @event = new EventBusConfig()
                {
                    ConnectionRetryCount = 5,
                    EventNameSuffix = "IntegrationEvent",
                    SubscriptionClinetAppName = "BasketService",
                    // Connection = new ConnectionFactory(),
                    eventBusType = EventBusType.RabbitMq
                };
                return EventBusFactory.Create(@event, sp);
            });
            services.AddTransient<OrderCreatedIntegrationEventsHandler>();
        }

        private void ConfigureSubscription(IServiceProvider serviceProvider)
        {
            var eventBus = serviceProvider.GetRequiredService<IEventBus>();
            eventBus.Subscribe<OrderCreatedIntegrationEvents, OrderCreatedIntegrationEventsHandler>();
        }
    }
}