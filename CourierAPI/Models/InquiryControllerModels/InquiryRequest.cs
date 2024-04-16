using Courier.Domain.Models;

namespace CourierAPI.Models
{
    public class InquiryRequest
    {
        public double Weight { get; set; }
        public int Width { get; set; }
        public int Length { get; set; }
        public int Height { get; set; }
        public Address SourceAddress { get; set; }
        public Address DestinationAddress { get; set; }
        public DateTime DeliveryDate { get; set; }
        public DateTime PickupDate { get; set; }
        public bool IsCompany { get; set; }
        public bool IsPriorityHigh { get; set; }
        public bool DeliveryAtWeekend { get; set; }
        public Inquiry MakeInquiry()
        {
            return new Inquiry()
            {
                Weight = Weight,
                Width = Width,
                Length = Length,
                Height = Height,
                SourceAddress = SourceAddress,
                DestinationAddress = DestinationAddress,
                DeliveryDate = DeliveryDate,
                PickupDate = PickupDate,
                IsCompany = IsCompany,
                IsPriorityHigh = IsPriorityHigh,
                DeliveryAtWeekend = DeliveryAtWeekend,
                User = null,
            };
        }
    }
}