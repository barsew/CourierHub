using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courier.Domain.Models
{
    public enum OrderStatus // do omówienia
    {
        Pending,
        Accepted,
        Rejected,
        Received,
        Delivered,
        CannotDeliver,
    }
}
