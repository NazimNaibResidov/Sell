using BasketService.Api.Core.Domain.Models;
using BasketService.Api.IntegrationEvents.Events;
using BasketService.Api.Interfaces;
using EventBus.Base.Abstrasctions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BasketService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepsotory basketRepsotory;
        private readonly IIdentityService identityService;
        private readonly IEventBus eventBus;
        private readonly ILogger<BasketController> logger;

        public BasketController(IBasketRepsotory basketRepsotory, IIdentityService identityService, IEventBus eventBus, ILogger<BasketController> logger)
        {
            this.basketRepsotory = basketRepsotory;
            this.identityService = identityService;
            this.eventBus = eventBus;
            this.logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Bakset Service is Up and Runging");
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CusomterBasket), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CusomterBasket>> GetBaksetByIdAsync(string id)
        {
            var result = await basketRepsotory.GetBasketAsync(id);
            return Ok(result ?? new CusomterBasket(id));
        }

        [HttpPost]
        [Route("update")]
        [ProducesResponseType(typeof(CusomterBasket), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CusomterBasket>> UpdateBaksetAsync([FromBody] CusomterBasket cusomterBasket)
        {
            return Ok(await basketRepsotory.UpdateBasketAsync(cusomterBasket));
        }

        [HttpPost]
        [Route("additem")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<CusomterBasket>> UpdateBaksetAsync([FromBody] BaksetItem baksetItem)
        {
            var userId = identityService.GetUserName();
            var basket = await basketRepsotory.GetBasketAsync(userId) ?? new CusomterBasket(userId);
            basket.Items.Add(baksetItem);
            await basketRepsotory.UpdateBasketAsync(basket);

            return Ok();
        }

        [HttpPost]
        [Route("checkout")]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult> CeckoutAsync([FromBody] BasketCheckout basketCheckout)
        {
            var userId = basketCheckout.Buyer;
            var basket = await basketRepsotory.GetBasketAsync(userId);
            if (basket != null)
            {
                return BadRequest();
            }
            var userName = identityService.GetUserName();
            var eventMessage = new OrderCreatedIntegrationEvents(userId, userName, basketCheckout.City, basketCheckout.Streat,
                basketCheckout.State, basketCheckout.Country, basketCheckout.ZipCode, basketCheckout.CartNumber, basketCheckout.CartHolderName, basketCheckout.CartExpriration, basketCheckout.CartSecurityNumber, basketCheckout.CartTypeId, basketCheckout.Buyer, basket);
            try
            {
                eventBus.Publish(eventMessage);
            }
            catch (Exception ex)
            {
                logger.LogInformation($"ERROR Published Integration Event :IntegrationEventId {eventMessage.ID} From BasketService.app :");
                logger.LogInformation($" THIS error inforation Message {ex.Message}");
                throw;
            }

            return Accepted();
        }
    }
}