using Microsoft.AspNetCore;
using OrderService.Api.Extensions;
using OrderService.Infrastructure.Context;

namespace BasketService.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(GetConfiguration(), args);
            host.MigrationDbContext<OrderDbContext>((context, service) =>
            {
                var logger = service.GetService<ILogger<OrderDbContext>>();
                var dbContextSeender = new OrderDbContextSeed();
                dbContextSeender.Seed(context, logger).Wait();
            });
            //CreateHostBuilder(args).Build().Run();
        }

        private static IWebHost BuildWebHost(IConfiguration configuration, string[] args) =>

           WebHost.CreateDefaultBuilder(args)
            .UseDefaultServiceProvider((context, options)
            =>
            {
                options.ValidateOnBuild = false;
            })
            .ConfigureAppConfiguration(x => x.AddConfiguration(configuration))
            .UseStartup<Startup>()
            .Build();

        private static IConfiguration GetConfiguration()
        {
            var builder = new ConfigurationBuilder()
                     .SetBasePath(Directory.GetCurrentDirectory())
                     .AddJsonFile("appsettings.json", optional: false, true)
                     .AddEnvironmentVariables();
            return builder.Build();
        }

        //public static IHostBuilder CreateHostBuilder(string[] args) =>
        //    Host.CreateDefaultBuilder(args)
        //        .ConfigureWebHostDefaults(webBuilder =>
        //        {
        //            webBuilder.UseStartup<Startup>();
        //        });
    }
}