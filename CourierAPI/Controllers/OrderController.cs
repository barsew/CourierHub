using Azure.Core;
using Courier.Domain.Models;
using Courier.Domain.Repository.IRepository;
using Courier.Domain.Services.Email;
using Courier.Domain.Services.OrderService;
using CourierAPI.Models;
using CourierAPI.Models.OrderControllerModels;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CourierAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOrderService _orderService;
        private readonly IValidator<Order> _validator;
        private readonly IEmailSender _emailSender;

        public OrderController(IUnitOfWork unitOfWork, IOrderService orderService, IValidator<Order> validator, IEmailSender emailSender)
        {
            _unitOfWork = unitOfWork;
            _orderService = orderService;
            _validator = validator;
            _emailSender = emailSender;
        }

        [HttpPost]
        [Authorize(Policy = "PostOrder")]
        [ProducesResponseType(typeof(OrderReponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        [ProducesResponseType((int)HttpStatusCode.Gone)]
        public ActionResult<OrderReponse> Post(OrderRequest request)
        {
            var receiver = _orderService.CreateSubjectForOrderData(request.ReceiverName, request.ReceiverSurname, request.ReceiverEmail, request.ReceiverPhone);
            var sender = _orderService.CreateSubjectForOrderData(request.SenderName, request.SenderSurname, request.SenderEmail, request.SenderPhone);

            var offer = _unitOfWork.Offer.Get(o => o.Id == request.OfferId);

            if (offer == null)
                return NoContent();
            if (offer.OfferStatus != OfferStatus.Offered)
                return Conflict("Offer is already ordered");
            if (offer.ExpireDate < DateTime.Now)
                return StatusCode(410, "Request offer has already expired");


            Order order = _orderService.MakeOrderForOffer(offer, sender, receiver);
            var validationResult = _validator.Validate(order);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.ToString());

            offer.OfferStatus = OfferStatus.Ordered;

            _unitOfWork.Order.Add(order);
            _unitOfWork.Save();

            string mailSubject = "Order has been placed";
            _emailSender.SendEmail(sender.Email, EmailTemplates.CreateEmailForOrder(order.Id, order.OrderStatus, sender.Name, sender.Surname, true), mailSubject);
            _emailSender.SendEmail(receiver.Email, EmailTemplates.CreateEmailForOrder(order.Id, order.OrderStatus, receiver.Name, receiver.Surname, true), mailSubject);

            var orderReponse =  new OrderReponse()
            {
                OrderId = order.Id
            };
            return Ok(orderReponse);
        }
        [HttpGet]
        [Authorize(Policy = "GetOrder")]
        [ProducesResponseType(typeof(OrderReponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        public ActionResult<GetOrderResponse> Get(Guid orderId)
        {
            var order = _unitOfWork.Order.Get(o => o.Id == orderId);
            if (order == null)
                return NoContent();

            var reponse = new GetOrderResponse(order);
            return Ok(reponse);
        }

    }
}
