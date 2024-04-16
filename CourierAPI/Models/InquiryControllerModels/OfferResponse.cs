using Courier.Domain.Models;

namespace CourierAPI.Models
{
    public class OfferResponse
    {
        public Guid OfferId { get; set; }
        public Price[] PriceBreakdown { get; set; }
        public Price FullPrice { get; set; }
        public DateTime ExpireDate { get; set; }
        public OfferResponse(Offer offer)
        {
            OfferId = offer.Id;
            FullPrice = new Price {
                Amount = offer.PriceBreakdown.ToList().Sum(i => i.Amount),
                Currency = (offer.PriceBreakdown.Count > 0)? offer.PriceBreakdown.First().Currency : Currency.PLN,
            };
            PriceBreakdown = offer.PriceBreakdown.ToArray();
            ExpireDate = offer.ExpireDate;
        }
    }
}
