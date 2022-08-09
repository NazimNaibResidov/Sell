using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace OrderService.Infrastructure.Context
{
    public class OrderDbContextDesingFacotry : IDesignTimeDbContextFactory<OrderDbContext>
    {
        public OrderDbContextDesingFacotry()
        {
        }

        public OrderDbContext CreateDbContext(string[] args)
        {
            var connectionString = "server=.;database=myDb;trusted_connection=true;";
            var optionsBuilder = new DbContextOptionsBuilder<OrderDbContext>()
                .UseSqlServer(connectionString);
            return new OrderDbContext(optionsBuilder.Options, new NoMediator());
        }
    }
}