using Microsoft.EntityFrameworkCore;
using Polly;
using System.Data.SqlClient;

namespace OrderService.Api.Extensions
{
    public static class WebHostExtension
    {
        public static IWebHost MigrationDbContext<TContext>(this IWebHost host, Action<TContext, IServiceProvider> seender)
            where TContext : DbContext
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<TContext>>();
                var context = services.GetService<TContext>();
                try
                {
                    logger.LogInformation($"migration database associaed with context {typeof(TContext).Name} ");
                    var retry = Policy.Handle<SqlException>()
                        .WaitAndRetry(new TimeSpan[]
                        {
TimeSpan.FromSeconds(1),
TimeSpan.FromSeconds(3),
TimeSpan.FromSeconds(5),
TimeSpan.FromSeconds(8)
                        });
                    retry.Execute(() => InvokeSeeker(seender, context, services));
                    logger.LogInformation("Migrated Database");
                }
                catch (Exception ex)
                {
                    logger.LogError($"an error occured while migration the database used on context {typeof(TContext).Name}");
                }
                return host;
            }
        }

        private static void InvokeSeeker<TContext>(Action<TContext, IServiceProvider> seender, TContext context, IServiceProvider services) where TContext : DbContext
        {
            context.Database.EnsureCreated();
            context.Database.Migrate();
            seender(context, services);
        }
    }
}