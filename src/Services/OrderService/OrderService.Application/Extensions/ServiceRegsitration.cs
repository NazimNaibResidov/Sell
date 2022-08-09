using MediatR;
using Microsoft.Extensions.DependencyInjection;
using OrderService.Application.Features.Commands.CreateOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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
