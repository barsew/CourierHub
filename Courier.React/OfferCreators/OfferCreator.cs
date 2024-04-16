using Courier.Domain.Models;

namespace Courier.React.OfferCreators
{
    public abstract class OfferCreator
    {
        public abstract Task<Offer> MakeOfferAsync(Inquiry inquiry);
    }
}
