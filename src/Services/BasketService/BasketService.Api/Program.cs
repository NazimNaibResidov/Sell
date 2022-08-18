using Microsoft.AspNetCore;
using Serilog;

namespace BasketService.Api
{
    public class Program
    {
        private static string env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        private static IConfiguration configuration
        {
            get
            {
                return new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile($"Confiugrations/appsettings.json", optional: false)
                    .AddJsonFile($"Confiugrations/appsettings.{env}.json", optional: true)
                    .AddEnvironmentVariables()
                    .Build();
            }
        }

        private static IConfiguration serliconfiguration
        {
            get
            {
                return new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile($"Confiugrations/serlog.json")
                    .AddJsonFile($"Confiugrations/serlog.{env}.json")
                    .AddEnvironmentVariables()
                    .Build();
            }
        }

        public static IWebHost BuildWebHost(IConfiguration configuration, string[] args)
        {
            return WebHost.CreateDefaultBuilder()
                .ConfigureAppConfiguration(i => i.AddConfiguration(configuration))
                .UseStartup<Startup>()
                .ConfigureLogging(x => x.ClearProviders())
                 .UseSerilog()
                .Build();
        }

        public static void Main(string[] args)
        {
            var host = BuildWebHost(configuration, args);
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(serliconfiguration)
                .CreateLogger();
            host.Run();
        }

        //public static IHostBuilder CreateHostBuilder(string[] args) =>
        //    Host.CreateDefaultBuilder(args)
        //        .ConfigureWebHostDefaults(webBuilder =>
        //        {
        //            webBuilder.UseStartup<Startup>();
        //        });
    }
}