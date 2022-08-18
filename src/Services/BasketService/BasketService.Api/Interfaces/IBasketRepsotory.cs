using BasketService.Api.Core.Domain.Models;

namespace BasketService.Api.Interfaces
{
    public interface IBasketRepsotory
    {
        IEnumerable<string> GetUsers();

        Task<CusomterBasket> GetBasketAsync(string cusomterId);

        Task<CusomterBasket> UpdateBasketAsync(CusomterBasket cusomterBasket);

        Task<bool> DeleteBasketAsync(string id);
    }
}