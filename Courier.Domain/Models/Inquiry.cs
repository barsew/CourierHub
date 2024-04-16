using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courier.Domain.Models
{
    public class Inquiry
    {
        public static string WeightUnit { get; } = "kg";
        public static string DimensionUnit { get; } = "cm";
        public Guid Id { get; set; }
        public double Weight { get; set; } // kg
        public int Width { get; set; } // cm
        public int Length { get; set; } // cm
        public int Height { get; set; } // cm
        public Address SourceAddress { get; set; }
        public Address DestinationAddress { get; set; }
        public DateTime DeliveryDate { get; set; }
        public DateTime PickupDate { get; set;}
        public bool IsCompany { get; set;}
        public bool IsPriorityHigh { get; set; }
        public bool DeliveryAtWeekend { get; set;}
        public User? User { get; set; }
        public DateTime PostedDate { get; set; } = DateTime.Now;
    }

}
