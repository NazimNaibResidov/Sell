using MediatR;
using OrderService.Application.Interfaces.Repostory;
using OrderService.Domain.AggregateModel.BuyerAggregate;
using OrderService.Domain.Events;

namespace OrderService.Application.DomainEventHandler
{
    public class OrderStartedDomainEventHandler : INotificationHandler<OrderStartedDomainEvent>
    {
        private readonly IBuyerRepsotory buyerRepository;

        public OrderStartedDomainEventHandler(IBuyerRepsotory buyerRepository)
        {
            this.buyerRepository = buyerRepository;
        }

        public async Task Handle(OrderStartedDomainEvent orderStartedEvent, CancellationToken cancellationToken)
        {
            var cartTypeId = (orderStartedEvent.CardTypeId != 0) ? orderStartedEvent.CardTypeId : 1;
            var buyer = await buyerRepository.GetSingleAsyc(x => x.Name == orderStartedEvent.UserName, i => i.PaymentMethods);
            var buyerOrginallyExits = buyer != null;
            if (!buyerOrginallyExits)
            {
                buyer = new Buyer(orderStartedEvent.UserName);
            }
            buyer.VerifyOrAddPaymentMethod(cartTypeId, orderStartedEvent.CardNumber, orderStartedEvent.CardNumber, orderStartedEvent.CardSecurityNumber, orderStartedEvent.CardHolderName, DateTime.UtcNow, orderStartedEvent.Order.Id);
            var buyerUpdate = buyerOrginallyExits ?
                buyerRepository.Update(buyer) :
                await buyerRepository.AddAsync(buyer);
            await buyerRepository.unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}