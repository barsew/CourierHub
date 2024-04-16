using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courier.Domain.Models
{
    public class Offer
    {
        public static TimeSpan ExpiryTime { get; } = new TimeSpan(3, 0, 0, 0);
        public Guid Id { get; set; }
        public Inquiry Inquiry { get; set; }
        public ICollection<Price> PriceBreakdown { get; set; }
        public Company Company { get; set; }
        public DateTime ExpireDate { get; set; }
        public OfferStatus OfferStatus { get; set; }
        public DateTime PostedDate { get; set; } = DateTime.Now;
    }
}
