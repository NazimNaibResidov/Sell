using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace OrderService.Application.Extensions
{
    public static class ServiceRegsitration
    {
        public static IServiceCollection AddApplicationRegistration(this IServiceCollection services, Type configuration)
        {
            var assamble = Assembly.GetExecutingAssembly();

            var assamble2 = configuration.GetType().Assembly;
            services.AddAutoMapper(assamble);
            //services.AddMediatR(assamble, assamble2, typeof(CreateOrderCommandHandler).GetTypeInfo().Assembly);
            services.AddMediatR(assamble);
            return services;
        }
    }
}