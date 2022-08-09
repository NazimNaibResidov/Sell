using OrderService.Domain.Exceptions;
using OrderService.Domain.SeedWork;

namespace OrderService.Domain.AggregateModel.BuyerAggregate
{
    public class PaymentMethod
    : BaseEntity
    {
        private string Alias;
        private string CardNumber;
        private string _securityNumber;
        private string _cardHolderName;
        private DateTime Expiration;

        private int CardTypeId;
        public CardType CardType { get; private set; }

        protected PaymentMethod()
        { }

        public PaymentMethod(int cardTypeId, string alias, string cardNumber, string securityNumber, string cardHolderName, DateTime expiration)
        {
            CardNumber = !string.IsNullOrWhiteSpace(cardNumber) ? cardNumber : throw new OrderingDomainException(nameof(cardNumber));
            _securityNumber = !string.IsNullOrWhiteSpace(securityNumber) ? securityNumber : throw new OrderingDomainException(nameof(securityNumber));
            _cardHolderName = !string.IsNullOrWhiteSpace(cardHolderName) ? cardHolderName : throw new OrderingDomainException(nameof(cardHolderName));

            if (expiration < DateTime.UtcNow)
            {
                throw new OrderingDomainException(nameof(expiration));
            }

            Alias = alias;
            Expiration = expiration;
            CardTypeId = cardTypeId;
        }

        public bool IsEqualTo(int cardTypeId, string cardNumber, DateTime expiration)
        {
            return CardTypeId == cardTypeId
                && CardNumber == cardNumber
                && Expiration == expiration;
        }
    }
}