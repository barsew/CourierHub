using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courier.Domain.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public OrderStatus OrderStatus { get; set; } 
        public Offer Offer { get; set; }
        public Subject Sender { get; set; }
        public Subject Receiver { get; set; }
        public int? Rating { get; set; }
        public DateTime PostedDate { get; set; } = DateTime.Now;
        public DateTime? DecisionDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
    }
}
