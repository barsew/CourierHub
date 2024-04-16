using Courier.Domain.Models;

namespace Courier.React.Models.ExternalAPIModels
{
    public class Dimensions
    {
        public double Width { get; set; }
        public double Height { get; set; }
        public double Length { get; set; }
        public string DimensionUnit { get; set; }
    }

    public class Address
    {
        public string HouseNumber { get; set; }
        public string ApartmentNumber { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }

        public Address(Courier.Domain.Models.Address address) 
        {
            HouseNumber = address.HouseNumber;
            ApartmentNumber = address.ApartmentNumber;
            Street = address.Street;
            City = address.City;
            ZipCode = address.ZipCode;
            Country = address.Country;
        }
    }

    public class InquiriesRequest
    {
        public Dimensions Dimensions { get; set; }
        public string Currency { get; set; }
        public double Weight { get; set; }
        public string WeightUnit { get; set; }
        public Address Source { get; set; }
        public Address Destination { get; set; }
        public DateTime PickupDate { get; set; }
        public DateTime DeliveryDay { get; set; }
        public bool DeliveryInWeekend { get; set; }
        public string Priority { get; set; }
        public bool VipPackage { get; set; }
        public bool IsCompany { get; set; }

        public InquiriesRequest(Inquiry inquiry)
        {
            Dimensions = new Dimensions()
            {
                Width = CmToMeter(inquiry.Width),
                Length = CmToMeter(inquiry.Length),
                Height = CmToMeter(inquiry.Height),
                DimensionUnit = "Meters"
            };
            Currency = "Pln";
            Weight = inquiry.Weight;
            WeightUnit = "Kilograms";
            Source = new Address(inquiry.SourceAddress);
            Destination = new Address(inquiry.DestinationAddress);
            PickupDate = inquiry.PickupDate;
            DeliveryDay = inquiry.DeliveryDate;
            DeliveryInWeekend = inquiry.DeliveryAtWeekend;
            Priority = ConvertPriority(inquiry.IsPriorityHigh);
            VipPackage = false;
            IsCompany = false;
        }
        private double CmToMeter(int cm)
        {
            return (double)cm / 100;
        }
        private string ConvertPriority(bool isHigh)
        {
            return (isHigh ? "High" : "Low");
        }
    }
}
