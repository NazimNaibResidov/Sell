using MediatR;
using OrderService.Domain.SeedWork;
using OrderService.Infrastructure.Context;

namespace OrderService.Infrastructure.Exceptions
{

    public static class MediatorException
    {
        public static async Task DispatchDomainEventsAsync(this IMediator mediator, OrderDbContext ctx)
        {
            var domainEntities = ctx.ChangeTracker
                .Entries<BaseEntity>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any())
                .ToList();
            var tasks = new List<Task>();
            while (domainEntities.Any())
            {
                var domainEvents = domainEntities
                    .SelectMany(x => x.Entity.DomainEvents)
                    .ToList();

                domainEntities.ForEach(async entity => entity.Entity.ClearDomainEvent());

                tasks.AddRange(domainEvents
                    .Select(async (domainEvent) =>
                    {
                        await mediator.Publish(domainEvent);
                    }));

                domainEntities = ctx.ChangeTracker
                    .Entries<BaseEntity>()
                    .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any())
                    .ToList();
            }
            await Task.WhenAll(tasks);
        }
    }
}