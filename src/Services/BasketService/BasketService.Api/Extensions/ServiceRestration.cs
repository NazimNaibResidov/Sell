using BasketService.Api.Core.Application.Service;
using BasketService.Api.Infrastrucutre.Repostorye;
using BasketService.Api.Interfaces;

namespace BasketService.Api.Extensions
{
    public static class ServiceRestration
    {
        public static IServiceCollection Services(this IServiceCollection servic)
        {
            servic.AddHttpContextAccessor();
            servic.AddScoped<IBasketRepsotory, BasketRepsotory>();
            servic.AddTransient<IIdentityService, IdentityService>();
            return servic;
        }
    }
}