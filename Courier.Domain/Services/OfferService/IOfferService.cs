using Courier.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courier.Domain.Services.OfferService
{
    public interface IOfferService
    {
        public Offer MakeOfferForInquiry(Inquiry inquiry, Company company);
    }
}
