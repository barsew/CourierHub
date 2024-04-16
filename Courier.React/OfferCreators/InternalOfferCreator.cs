using Courier.Domain.Models;
using Courier.Domain.Repository.IRepository;
using Courier.Domain.Services.OfferService;

namespace Courier.React.OfferCreators
{
    public class InternalOfferCreator : OfferCreator
    {
        private readonly IOfferService _offerService;
        private readonly IUnitOfWork _unitOfWork;
        public InternalOfferCreator(IOfferService offerService, IUnitOfWork unitOfWork)
        {
            _offerService = offerService;
            _unitOfWork = unitOfWork;
        }

        async public override Task<Offer> MakeOfferAsync(Inquiry inquiry)
        {
            var offer = _offerService.MakeOfferForInquiry(inquiry,
                _unitOfWork.Company.Get((Company c) => c.Id == Company.OurCompany.Id));
            return offer;
        }
    }
}
