using Courier.Domain.Models;
using System.Text.Json.Serialization;

namespace Courier.React.Models.OrderControllerModels
{
    public class GetOrderResponse
    {
        public Guid OrderId { get; set; }
        public double Weight { get; set; }
        public string WeightUnit { get; set; } = Inquiry.WeightUnit;
        public int Width { get; set; }
        public int Length { get; set; }
        public int Height { get; set; }
        public string DimensionUnit { get; set; } = Inquiry.DimensionUnit;
        public Address SourceAddress { get; set; }
        public Address DestinationAddress { get; set; }
        public DateTime DeliveryDate { get; set; }
        public DateTime PickupDate { get; set; }
        public bool IsCompany { get; set; }
        public bool IsPriorityHigh { get; set; }
        public bool DeliveryAtWeekend { get; set; }
        public Price[] PriceBreakdown { get; set; }
        public Price FullPrice { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public OrderStatus OrderStatus { get; set; }
        public DateTime OfferDate { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? DecisionDate { get; set; }
        public string SenderName { get; set; }
        public string SenderSurname { get; set; }
        public string SenderEmail { get; set; }
        public string SenderPhone { get; set; }
        public string ReceiverName { get; set; }
        public string ReceiverSurname { get; set; }
        public string ReceiverEmail { get; set; }
        public string ReceiverPhone { get; set; }

        public GetOrderResponse(Order order)
        {
            Offer offer = order.Offer;
            Inquiry inquiry = offer.Inquiry;
            Subject sender = order.Sender;
            Subject receiver = order.Receiver;

            OrderId = order.Id;
            Weight = inquiry.Weight;
            Width = inquiry.Width;
            Length = inquiry.Length;
            Height = inquiry.Height;
            SourceAddress = inquiry.SourceAddress;
            DestinationAddress = inquiry.DestinationAddress;
            DeliveryDate = inquiry.DeliveryDate;
            PickupDate = inquiry.PickupDate;
            IsCompany = inquiry.IsCompany;
            IsPriorityHigh = inquiry.IsPriorityHigh;
            DeliveryAtWeekend = inquiry.DeliveryAtWeekend;
            PriceBreakdown = offer.PriceBreakdown.ToArray();
            FullPrice = new Price
            {
                Amount = offer.PriceBreakdown.ToList().Sum(i => i.Amount),
                Currency = offer.PriceBreakdown.First().Currency,
            };
            OrderStatus = order.OrderStatus;
            OfferDate = offer.PostedDate;
            OrderDate = order.PostedDate;
            DecisionDate = order.DecisionDate;
            SenderName = sender.Name;
            SenderSurname = sender.Surname;
            SenderEmail = sender.Email;
            SenderPhone = sender.Phone;
            ReceiverName = receiver.Name;
            ReceiverSurname = receiver.Surname;
            ReceiverEmail = receiver.Email;
            ReceiverPhone = receiver.Phone;
        }
    }

}
