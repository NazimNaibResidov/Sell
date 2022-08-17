using CatalogService.Api.Infrastrcuture.Context;
using Consul;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CatalogService.Api.Extensions
{
    public static class ConsuleExtension
    {
        public static IServiceCollection RegistrationConsule(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IConsulClient, ConsulClient>(sp => new ConsulClient(c =>
            {
                var address = configuration["consuleConfig:address"];
                c.Address = new Uri(address);
            }));
            return services;
        }
        public static IApplicationBuilder RegisterWithConsule(this IApplicationBuilder app, IHostApplicationLifetime lifetime)
        {
            var consulClient = app.ApplicationServices.GetRequiredService<IConsulClient>();
            var loggerFactory = app.ApplicationServices.GetRequiredService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger<IApplicationBuilder>();
            var features = app.Properties["server:Features"] as FeatureCollection;
            var adresses = features.Get<IServerAddressesFeature>();
            var adress = adresses.Addresses.First();
            var url = new Uri(adress);
            var resistrations = new AgentServiceRegistration()
            {
                ID = $"CatalogService",
                Name = $"CatalogService",
                Address = $"{url.Host}",
                Port = url.Port,
                Tags = new[] { "Catalog Service", "Catalog" }
            };
            logger.LogInformation("Registration With Consule");
            consulClient.Agent.ServiceDeregister(resistrations.ID).Wait();
            consulClient.Agent.ServiceRegister(resistrations).Wait();
            lifetime.ApplicationStopping.Register(() =>
            {
                logger.LogInformation("Deregisterion from consule");
                consulClient.Agent.ServiceDeregister(resistrations.ID).Wait();
            });
            return app;


        }
        public static IApplicationBuilder RegisterWithConsule(this IApplicationBuilder app, IHostApplicationLifetime lifetime,string IdName,string Name)
        {
            var consulClient = app.ApplicationServices.GetRequiredService<IConsulClient>();
            var loggerFactory = app.ApplicationServices.GetRequiredService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger<IApplicationBuilder>();
            var features = app.Properties["server:Features"] as FeatureCollection;
            var adresses = features.Get<IServerAddressesFeature>();
            var adress = adresses.Addresses.First();
            var url = new Uri(adress);
            var resistrations = new AgentServiceRegistration()
            {
                ID = $"{IdName}",
                Name = $"{Name}",
                Address = $"{url.Host}",
                Port = url.Port,
                Tags = new[] { "Catalog Service", "Catalog" }
            };
            logger.LogInformation("Registration With Consule");
            consulClient.Agent.ServiceDeregister(resistrations.ID).Wait();
            consulClient.Agent.ServiceRegister(resistrations).Wait();
            lifetime.ApplicationStopping.Register(() =>
            {
                logger.LogInformation("Deregisterion from consule");
                consulClient.Agent.ServiceDeregister(resistrations.ID).Wait();
            });
            return app;


        }
    }
    public static class DbContextRegistration
    {
        public static IServiceCollection ConfigureDbContex(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddEntityFrameworkSqlServer()
                .AddDbContext<CatalogContext>(options =>
                {
                    var connectionString = configuration.GetConnectionString("catalog");
                    options.UseSqlServer(connectionString,
                                         sqlServerOptionsAction: sqlOptions =>
                                         {
                                             sqlOptions.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
                                             sqlOptions.EnableRetryOnFailure(15, System.TimeSpan.FromSeconds(10), null);
                                         });
                });
            return services;
        }
    }
}