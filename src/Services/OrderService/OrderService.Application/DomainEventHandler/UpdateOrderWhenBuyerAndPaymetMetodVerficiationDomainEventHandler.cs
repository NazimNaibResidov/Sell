using MediatR;
using OrderService.Application.Interfaces.Repostory;
using OrderService.Domain.Events;

namespace OrderService.Application.DomainEventHandler
{
    public class UpdateOrderWhenBuyerAndPaymetMetodVerficiationDomainEventHandler : INotificationHandler<BuyerAndPaymentMethodVerifiedDomainEvent>
    {
        private readonly IOrderRepsotory repsotory;

        public UpdateOrderWhenBuyerAndPaymetMetodVerficiationDomainEventHandler(IOrderRepsotory repsotory)
        {
            this.repsotory = repsotory?? throw new ArgumentNullException(nameof(repsotory));
        }

        public async Task Handle(BuyerAndPaymentMethodVerifiedDomainEvent buyerAndPaymentMethodVerifiedDomainEvent, CancellationToken cancellationToken)
        {
            var orderToUpdate = await repsotory.GetByIdAsyc(buyerAndPaymentMethodVerifiedDomainEvent.OrderId);
            orderToUpdate.SetBuyerId(buyerAndPaymentMethodVerifiedDomainEvent.Buyer.Id);
            orderToUpdate.SetPaymentId(buyerAndPaymentMethodVerifiedDomainEvent.Payment.Id);
            
        }
    }
}