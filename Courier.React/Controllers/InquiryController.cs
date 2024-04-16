using Azure.Core;
using Courier.Domain.Models;
using Courier.Domain.Repository;
using Courier.Domain.Repository.IRepository;
using Courier.Domain.Services.OfferService;
using Courier.React.ExternalAPIClients;
using Courier.React.OfferCreators;
using CourierAPI.Models;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Courier.React.Controllers
{
    [ApiController]
    [Route("api2/[controller]")]
    public class InquiryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOfferService _offerService;
        IValidator<Inquiry> _validator;

        public InquiryController(IUnitOfWork unitOfWork, IOfferService offerService, IValidator<Inquiry> validator)
        {
            _unitOfWork = unitOfWork;
            _offerService = offerService;
            _validator = validator;
        }

        [Authorize(Policy = "GetAllInquiries")]
        [HttpGet]
        public ActionResult<IEnumerable<Inquiry>> Get()
        {
            var inquiries = _unitOfWork.Inquiry.GetAll();
            return Ok(inquiries);
        }

        [Authorize]
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<Inquiry>> Get(string id)
        {
            var inquiries = _unitOfWork.Inquiry.GetAll(i => i.User != null && i.User.Auth0Id == id);
            return Ok(inquiries);
        }

        [HttpPost]
        public async Task<ActionResult<List<OfferResponse>>> Post(InquiryRequest request)
        {
            var inquiry = request.MakeInquiry();
            var validationResult = _validator.Validate(inquiry);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.ToString());

            if(Client.IsUserLogged)
                inquiry.User = _unitOfWork.User.Get((User u) => u.Id == Client.ActiveUser.Id);

            _unitOfWork.Inquiry.Add(inquiry);

            var offers = await MakeOffersAsync(inquiry);
            var offerResponses = new List<OfferResponse>();
            foreach(var offer in offers)
            {
                _unitOfWork.Offer.Add(offer);
                offerResponses.Add(new OfferResponse(offer));
            }
            _unitOfWork.Save();

            return Ok(offerResponses);
        }

        async private Task<List<Offer>> MakeOffersAsync(Inquiry inquiry)
        {
            List<OfferCreator> creators = new List<OfferCreator>();
            creators.Add(new InternalOfferCreator(_offerService, _unitOfWork));
            creators.Add(new ExternalOfferCreator(_unitOfWork));
            List<Task<Offer>> tasks = new List<Task<Offer>>();
            foreach (var creator in creators)
            {
                tasks.Add(creator.MakeOfferAsync(inquiry));
            }
            await Task.WhenAll(tasks);
            return tasks.Where(t => t.Result != null).Select(t => t.Result).ToList();
        }

    }
}
