using EventBus.Base.Abstrasctions;
using MediatR;
using OrderService.Application.IntegrationEvents;
using OrderService.Application.Interfaces.Repostory;
using OrderService.Application.ViewModels;
using OrderService.Domain.AggregateModel.OrderAggreage;

namespace OrderService.Application.Features.Commands.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, bool>
    {
        private readonly IOrderRepsotory repsotory;
        private readonly IEventBus mapper;

        public CreateOrderCommandHandler(IOrderRepsotory repsotory, IEventBus mapper)
        {
            this.repsotory = repsotory;
            this.mapper = mapper;
        }

        public async Task<bool> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var addr = new Address(request.Street, request.City, request.State, request.Contry, request.ZipCode);
            Order Dborder = new Order(request.UserName, addr, request.CartTypeId, request.CartNumber, request.CartSecurityNumber, request.CartHoldName, request.CartExpresion, null);
            request.OrderItems.ToList()
                .ForEach(i => Dborder.AddOrderItem(i.ProductId, i.ProductName, i.Unitprice, i.PictureUrl, i.Untis));
            foreach (var item in Dborder.OrderItems)
            {
                Dborder.AddOrderItem(item.ProductId, item.ProductName, item.UnitPrice, item.PictureUrl, item.Units);
            }
            await repsotory.AddAsync(Dborder);
            await repsotory.unitOfWork.SaveChangesAsync(cancellationToken);
            var orderStartedIntegrationEvent = new OrderStartedIntegrationEvent(request.UserName);
            mapper.Publish(orderStartedIntegrationEvent);
            return true;
        }
    }
}