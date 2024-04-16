using Courier.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courier.Domain.Services.OrderService
{
    public interface IOrderService
    {
        public Subject CreateSubjectForOrderData(string name, string surname, string email, string phone);
        public Order MakeOrderForOffer(Offer offer, Subject sender, Subject receiver);
    }
}
