using Courier.Domain.Models;
using Courier.Domain.Repository.IRepository;
using Courier.Domain.Services.OfferService;
using Courier.React.ExternalAPIClients;
using Courier.React.Models.ExternalAPIModels;

namespace Courier.React.OfferCreators
{
    public class ExternalOfferCreator : OfferCreator
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ExternalAPIClient _externalAPIClient;

        public ExternalOfferCreator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _externalAPIClient = ExternalAPIClient.GetInstance();
        }

        async public override Task<Offer?> MakeOfferAsync(Inquiry inquiry)
        {
            InquiriesRequest inquiryRequest = new InquiriesRequest(inquiry);
            var inquiriesResponse = await _externalAPIClient.MakeInquiriesCallAsync(inquiryRequest);
            if (inquiriesResponse == null)
                return null;
            Company company = _unitOfWork.Company.Get((Company c) => c.Id == Company.TheOtherCompany.Id);

            return new Offer()
            {
                Inquiry = inquiry,
                PriceBreakdown = inquiriesResponse.PriceBreakDown.Select(pbd => pbd.ConvertToPriceModel()).ToList(),
                Company = company,
                ExpireDate = inquiriesResponse.ExpiringAt,
                OfferStatus = OfferStatus.Offered
            };
        }
    }
}
