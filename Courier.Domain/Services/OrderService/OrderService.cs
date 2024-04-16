using Courier.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courier.Domain.Services.OrderService
{
    public class OrderService : IOrderService
    {
        public Subject CreateSubjectForOrderData(string name, string surname, string email, string phone)
        {
            return new Subject()
            {
                Name = name,
                Surname = surname,
                Email = email,
                Phone = phone
            };
        }

        public Order MakeOrderForOffer(Offer offer, Subject sender, Subject receiver)
        {
            return new Order()
            {
                OrderStatus = OrderStatus.Pending,
                Offer = offer,
                Sender = sender,
                Receiver = receiver
            };
        }
    }
}
