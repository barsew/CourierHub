using Courier.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courier.Domain.Services.Calculations
{
    public static class OfferCalculation
    {
        private const Decimal basePrice = 10.00m;
        private const int baseVolume = 8000;
        private const double baseWeight = 1;
        private const Decimal priorityCoef = 0.25m;
        private const Decimal weekendCoef = 0.5m;
        private const Decimal taxCoef = 0.01m;

        public static List<Price> CalculatePriceForInquiry(Inquiry inquiry)
        {
            List<Price> PriceBreakdown = new List<Price>();
            var packagePrice = CalculatePackagePrice(inquiry);
            PriceBreakdown.Add(packagePrice);
            if (inquiry.IsPriorityHigh)
                PriceBreakdown.Add(new Price()
                {
                    Amount = (Decimal)(priorityCoef * packagePrice.Amount),
                    Currency = Price.DefaultCurrency,
                    Description = "Priority package"
                });
            if (inquiry.DeliveryAtWeekend)
                PriceBreakdown.Add(new Price()
                {
                    Amount = (Decimal)(weekendCoef * packagePrice.Amount),
                    Currency = Price.DefaultCurrency,
                    Description = "Delivery at weekend"
                });
            PriceBreakdown.Add(new Price()
            {
                Amount = (Decimal)(taxCoef * packagePrice.Amount),
                Currency = Price.DefaultCurrency,
                Description = "Tax"
            });
            return PriceBreakdown;
        }
        private static Price CalculatePackagePrice(Inquiry inquiry)
        {
            Decimal price = basePrice;
            price *= (Decimal)(inquiry.Weight / baseWeight);
            price *= (Decimal)(inquiry.Width * inquiry.Weight * inquiry.Height / baseVolume);
            if (price < basePrice)
                price = basePrice;
            return new Price()
            {
                Amount = price,
                Currency = Price.DefaultCurrency,
                Description = "Package price"
            };
        }
    }
}
