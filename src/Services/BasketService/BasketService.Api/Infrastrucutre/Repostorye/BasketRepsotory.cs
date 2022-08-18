using BasketService.Api.Core.Domain.Models;
using BasketService.Api.Interfaces;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace BasketService.Api.Infrastrucutre.Repostorye
{
    public class BasketRepsotory : IBasketRepsotory
    {
        private readonly ILogger<BasketRepsotory> _logger;
        private readonly ConnectionMultiplexer _redis;
        private readonly IDatabase database;

        public BasketRepsotory(ILoggerFactory logger, ConnectionMultiplexer connection)
        {
            _logger = logger.CreateLogger<BasketRepsotory>();
            this._redis = connection;
            this.database = connection.GetDatabase();
        }

        public async Task<bool> DeleteBasketAsync(string id)
        {
            return await database.KeyDeleteAsync(id);
        }

        public async Task<CusomterBasket> GetBasketAsync(string cusomterId)
        {
            var result = await database.StringGetAsync(cusomterId);
            if (result.IsNullOrEmpty)
            {
                return null;
            }
            return JsonConvert.DeserializeObject<CusomterBasket>(result);
        }

        public IEnumerable<string> GetUsers()
        {
            var service = GetServer();
            var data = service.Keys();
            return data.Select(x => x.ToString());
        }

        public async Task<CusomterBasket> UpdateBasketAsync(CusomterBasket cusomterBasket)
        {
            var SerializeObject = JsonConvert.SerializeObject(cusomterBasket);
            var created = await database.StringSetAsync(cusomterBasket.BuyerId, SerializeObject);
            if (!created)
            {
                _logger.LogInformation("Problem occrued parsin the item");
                return null;
            }
            _logger.LogInformation("Busket item parsin successfull");
            return await GetBasketAsync(cusomterBasket.BuyerId);
        }

        private IServer GetServer()
        {
            var redis = _redis.GetEndPoints();
            return _redis.GetServer(redis.First());
        }
    }
}