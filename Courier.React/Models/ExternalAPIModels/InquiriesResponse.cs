using Courier.Domain.Models;

namespace Courier.React.Models.ExternalAPIModels
{
    public class PriceBreakdown
    {
        public double Amount { get; set; }
        public string Currency { get; set; }
        public string Description { get; set; }

        public Price ConvertToPriceModel()
        {
            return new Price
            {
                Amount = (Decimal)this.Amount,
                Currency = GetCurrencyEnum(),
                Description = this.Description
            };
        }
        private Currency GetCurrencyEnum()
        {
            switch(Currency.ToLower())
            {
                case "pln":
                    return Domain.Models.Currency.PLN;
                case "usd":
                    return Domain.Models.Currency.USD;
                case "eur":
                    return Domain.Models.Currency.EUR;
                default:
                    throw new Exception("Currency not found");
            };
        }

    }

    public class InquiriesResponse
    {
        public Guid InquiryId { get; set; }
        public double TotalPrice { get; set; }
        public string Currency { get; set; }
        public DateTime ExpiringAt { get; set; }
        public List<PriceBreakdown> PriceBreakDown { get; set; }
    }
}
