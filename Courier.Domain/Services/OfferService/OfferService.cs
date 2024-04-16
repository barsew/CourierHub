using Courier.Domain.Models;
using Courier.Domain.Services.Calculations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courier.Domain.Services.OfferService
{
    public class OfferService : IOfferService
    {
        public Offer MakeOfferForInquiry(Inquiry inquiry, Company company)
        {
            Offer offer = new Offer()
            {
                Inquiry = inquiry,
                PriceBreakdown = OfferCalculation.CalculatePriceForInquiry(inquiry),
                OfferStatus = OfferStatus.Offered,
                Company = company,
                ExpireDate = DateTime.Now + Offer.ExpiryTime
            };
            return offer;
        }
    }
}
