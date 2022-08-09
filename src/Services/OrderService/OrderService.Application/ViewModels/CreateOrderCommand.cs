using MediatR;

namespace OrderService.Application.ViewModels
{
    public class CreateOrderCommand : IRequest<bool>
    {
        public CreateOrderCommand(List<OrderItemDTO> orderItems)
        {
            _orderItems = orderItems;
        }

        public CreateOrderCommand(string userName, string city, string street, string state, string contry, string status, string zipCode, string descrriptiond, DateTime cartExpresion, string cartNumber, string cartHoldName, string cartSecurityNumber, string carrelationId, int cartTypeId)
        {
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
        }

        private List<OrderItemDTO> _orderItems { get; set; }
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

        public IEnumerable<OrderItemDTO> OrderItems => _orderItems;
        public decimal Total { get; set; }
    }
}