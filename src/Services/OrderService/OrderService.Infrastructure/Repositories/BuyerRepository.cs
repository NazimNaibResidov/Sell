using OrderService.Application.Interfaces.Repostory;
using OrderService.Domain.AggregateModel.BuyerAggregate;
using OrderService.Infrastructure.Context;

namespace OrderService.Infrastructure.Repositories
{
    public class BuyerRepository : GenericRepository<Buyer>, IBuyerRepsotory
    {
        public BuyerRepository(OrderDbContext context) : base(context)
        {
        }
    }
}