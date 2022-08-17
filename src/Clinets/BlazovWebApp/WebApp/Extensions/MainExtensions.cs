using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;

namespace WebApp.Extensions
{
    public static class MainExtensions
    {
        public static IServiceCollection SetHttpClinet(this IServiceCollection services)
        {
            services.AddScoped(sp =>
            {
                var clinetFactory = sp.GetRequiredService<IHttpClientFactory>();
                return clinetFactory.CreateClient("ApiGetwayHttpClinet");
            });
            services.AddHttpClient("ApiGetwayHttpClinet", clinet =>
            {
                clinet.BaseAddress = new Uri("http://localhost:5000/");
            });
            return services;
        }
    }
}