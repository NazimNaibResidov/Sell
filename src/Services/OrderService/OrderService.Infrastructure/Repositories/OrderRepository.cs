using OrderService.Application.Interfaces.Repostory;
using OrderService.Domain.AggregateModel.OrderAggreage;
using OrderService.Infrastructure.Context;
using System.Linq.Expressions;

namespace OrderService.Infrastructure.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepsotory
    {
        private readonly OrderDbContext context;

        public OrderRepository(OrderDbContext context) : base(context)
        {
            this.context = context;
        }

        public override async Task<Order> GetByIdAsyc(Guid id, params Expression<Func<Order, object>>[] include)
        {
            var query = await base.GetByIdAsyc(id, include);
            if (query == null)
            {
                query = context.Orders.Local.FirstOrDefault(x => x.Id == id);
            }
            return query;
        }
    }
}