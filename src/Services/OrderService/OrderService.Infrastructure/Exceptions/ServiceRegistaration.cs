using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderService.Application.Interfaces.Repostory;
using OrderService.Infrastructure.Context;
using OrderService.Infrastructure.Repositories;

namespace OrderService.Infrastructure.Exceptions
{
    public static class ServiceRegistaration
    {
        public static IServiceCollection AddPersistenceRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<OrderDbContext>(ops =>
            {
                ops.UseSqlServer("server=.;database=myDb;trusted_connection=true;");
                ops.EnableSensitiveDataLogging();
            });
            services.AddScoped<IBuyerRepsotory, BuyerRepository>();
            services.AddScoped<IOrderRepsotory, OrderRepository>();
            var optionsBuilder = new DbContextOptionsBuilder<OrderDbContext>
                ().UseSqlServer("server=.;database=myDb;trusted_connection=true;");
            var dbContext = new OrderDbContext(optionsBuilder.Options, null);
            dbContext.Database.EnsureCreated();
            dbContext.Database.Migrate();
            return services;
        }
    }
}