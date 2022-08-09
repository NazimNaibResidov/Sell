using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderService.Domain.AggregateModel.BuyerAggregate;
using OrderService.Domain.AggregateModel.OrderAggreage;
using OrderService.Domain.Interfaces;
using OrderService.Infrastructure.Exceptions;
using System.Reflection;

namespace OrderService.Infrastructure.Context
{
    public class OrderDbContext : DbContext, IUnitOfWork
    {
        private IMediator mediator;
        public const string DEFAULT_SCHEMA = "ordering";

        public OrderDbContext()
        {
        }

        public OrderDbContext(DbContextOptions<OrderDbContext> options, IMediator mediator) : base(options)
        {
            this.mediator = mediator;
        }

        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<CardType> CardTypes { get; set; }
        public DbSet<PaymentMethod> Payments { get; set; }
        public DbSet<Buyer> Buyers { get; set; }

        public async Task<bool> SetEntityasync(CancellationToken cancellationToken = default)
        {
            await mediator.DispatchDomainEventsAsync(this);
            await SaveChangesAsync(cancellationToken);
            return true;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}