using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderService.Application.Features.Queries.GetOrderbyId;

namespace OrderService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator medator;

        public OrderController(IMediator medator)
        {
            this.medator = medator;
        }

        [HttpGet("Id")]
        public async Task<IActionResult> GetOrderDetailsQueryById(Guid Id)
        {
           
            var res =await medator.Send(new GetOrderDetailsQuery(Id));
            return Ok(res);
        }
    }
}
