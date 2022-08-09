using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OrderService.Domain.AggregateModel.BuyerAggregate;
using OrderService.Domain.AggregateModel.OrderAggreage;
using OrderService.Domain.SeedWork;
using Polly;
using Polly.Retry;

namespace OrderService.Infrastructure.Context
{
    public class OrderDbContextSeed
    {
        public async Task Seed(OrderDbContext context, ILogger<OrderDbContext> logger)
        {
            var policy = CreatePoicy(logger, nameof(OrderDbContextSeed));
            await policy.ExecuteAsync(async () =>
            {
                var useCusomterData = false;
                var contentRootPath = "seeding/Setup";
                using (context)
                {
                    context.Database.Migrate();
                    if (!context.CardTypes.Any())
                    {
                        context.CardTypes.AddRange(useCusomterData ? GetCartFormFile(contentRootPath, logger)
                            : GetPrefeFindCardTypes());
                        await context.SaveChangesAsync();
                    }
                    if (!context.OrderStatuses.Any())
                    {
                        context.OrderStatuses.AddRange(useCusomterData ? GetOrdersFormFile(contentRootPath, logger)
                            : GetPrefeFindOrderStatus());
                        await context.SaveChangesAsync();
                    }
                }
            });
        }

        private IEnumerable<CardType> GetCartFormFile(string contentPath, ILogger<OrderDbContext> logger)
        {
            string fileName = "cartType.txt";
            if (!File.Exists(fileName))
            {
                return GetPrefeFindCardTypes();
            }
            var fileContent = File.ReadAllLines(fileName);
            var id = 1;
            var list = fileContent.Select(i => new CardType(id++, i)).Where(i => i != null);
            return list;
        }

        private IEnumerable<OrderStatus> GetOrdersFormFile(string contentPath, ILogger<OrderDbContext> logger)
        {
            string fileName = "orderStatus.txt";
            if (!File.Exists(fileName))
            {
                return GetPrefeFindOrderStatus();
            }
            var fileContent = File.ReadAllLines(fileName);
            var id = 1;
            var list = fileContent.Select(i => new OrderStatus(id++, i)).Where(i => i != null);
            return list;
        }

        private IEnumerable<CardType> GetPrefeFindCardTypes()
        {
            return Enumeration.GetAll<CardType>();
        }

        private IEnumerable<OrderStatus> GetPrefeFindOrderStatus()
        {
            return new List<OrderStatus>()
            { OrderStatus.Submitted,OrderStatus.AwaitingValidation,OrderStatus.StockConfirmed,OrderStatus.Paid,OrderStatus.Shipped,OrderStatus.Cancelled};
        }

        private AsyncRetryPolicy CreatePoicy(ILogger<OrderDbContext> logger, string prefix, int retryCount = 3)
        {
            return Policy.Handle<SqlException>()
    .WaitAndRetryAsync(retryCount: retryCount, sleepDurationProvider: _ => TimeSpan.FromSeconds(5),
    onRetry: (exception, timeSpan, retryCount, context) =>
    {
        logger.LogWarning("Retrying... " + retryCount);
    });
        }
    }
}