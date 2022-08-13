using EventBus.Base.Events;
using OrderService.Domain.Models;

namespace OrderService.Api.IntegrationEvents.Events
{
    public class OrderCreateIntegrationEvent:IntegrationEvent
    {
        public OrderCreateIntegrationEvent(string userId, string userName, string city, string street, string state, string contry, string status, string zipCode, string descrriptiond, DateTime cartExpresion, string cartNumber, string cartHoldName, string cartSecurityNumber, string carrelationId, int cartTypeId, int buyer, Guid requestId, CusomterBasket cusomterBasket)
        {
            UserId = userId;
            UserName = userName;
            City = city;
            Street = street;
            State = state;
            Contry = contry;
            Status = status;
            ZipCode = zipCode;
            Descrriptiond = descrriptiond;
            CartExpresion = cartExpresion;
            CartNumber = cartNumber;
            CartHoldName = cartHoldName;
            CartSecurityNumber = cartSecurityNumber;
            CarrelationId = carrelationId;
            CartTypeId = cartTypeId;
            Buyer = buyer;
            this.requestId = requestId;
            Basket = cusomterBasket;
        }

        public string UserId { get; set; }
        public string UserName { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string State { get; set; }
        public string Contry { get; set; }

        public string Status { get; set; }
        public string ZipCode { get; set; }
        public string Descrriptiond { get; set; }
        public DateTime CartExpresion { get; set; }
        public string CartNumber { get; set; }
        public string CartHoldName { get; set; }
        public string CartSecurityNumber { get; set; }
        public string CarrelationId { get; set; }
        public int CartTypeId { get; set; }
        public int Buyer { get; set; }
        public Guid  requestId { get; set; }
        public CusomterBasket Basket { get; set; }
    }
}
