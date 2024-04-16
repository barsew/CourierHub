using Courier.Domain.Services.OfferService;
using CourierAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Courier.Domain.Models;
using Courier.Domain.Repository.IRepository;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;

namespace CourierAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InquiryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOfferService _offerService;
        private readonly IValidator<Inquiry> _validator;

        public InquiryController(IUnitOfWork unitOfWork, IOfferService offerService, IValidator<Inquiry> validator)
        {
            this._unitOfWork = unitOfWork;
            this._offerService = offerService;
            _validator = validator;
        }

        [HttpPost]
        [Authorize(Policy = "PostInquiry")]
        [ProducesResponseType(typeof(OfferResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        public ActionResult<OfferResponse> PostInquiry([FromBody] InquiryRequest request)
        {
            var inquiry = request.MakeInquiry();

            var validationResult = _validator.Validate(inquiry);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.ToString());

            _unitOfWork.Inquiry.Add(inquiry);

            var offer = _offerService.MakeOfferForInquiry(inquiry, 
                _unitOfWork.Company.Get((Company c) => c.Id == Company.TheOtherCompany.Id));
            _unitOfWork.Offer.Add(offer);
            _unitOfWork.Save();

            var offerResponse = new OfferResponse(offer);
            return Ok(offerResponse);
        }
    }
}
