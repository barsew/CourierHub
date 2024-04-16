using Courier.Domain.Models;
using Courier.Domain.Repository;
using Courier.Domain.Repository.IRepository;
using Courier.Domain.Services.Email;
using Courier.Domain.Services.OrderService;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace Courier.React.Controllers
{
    [ApiController]
    [Route("api2/[controller]")]
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
        public ActionResult<Models.OrderControllerModels.OrderReponse> Post(Models.OrderControllerModels.OrderRequest request)
        {
            var sender = _orderService.CreateSubjectForOrderData(request.SenderName, request.SenderSurname, request.SenderEmail, request.SenderPhone);
            var receiver = _orderService.CreateSubjectForOrderData(request.ReceiverName, request.ReceiverSurname, request.ReceiverEmail, request.ReceiverPhone);

            var offer = _unitOfWork.Offer.Get(o => o.Id == request.OfferId);

            if (offer == null)
                return NoContent();

            Order order = _orderService.MakeOrderForOffer(offer, sender, receiver);
            var validationResult = _validator.Validate(order);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.ToString());
            if (offer.OfferStatus != OfferStatus.Offered)
                return Conflict("Offer is already ordered");
            if (offer.ExpireDate < DateTime.Now)
                return StatusCode(410, "Request offer has already expired");

            offer.OfferStatus = OfferStatus.Ordered;

            if (Client.IsUserLogged)
            {
                var inquiry = order.Offer.Inquiry;
                inquiry.User = _unitOfWork.User.Get((User u) => u.Id == Client.ActiveUser.Id);
            }

            _unitOfWork.Order.Add(order);
            _unitOfWork.Save();

            string mailSubject = "Order has been placed";
            _emailSender.SendEmail(sender.Email, EmailTemplates.CreateEmailForOrder(order.Id, order.OrderStatus, sender.Name, sender.Surname, true), mailSubject);
            _emailSender.SendEmail(receiver.Email, EmailTemplates.CreateEmailForOrder(order.Id, order.OrderStatus, receiver.Name, receiver.Surname, true), mailSubject);

            var response = new Models.OrderControllerModels.OrderReponse()
            {
                OrderId = order.Id
            };
            return Ok(Response);
        }

        [Authorize]
        [HttpGet("{id}")]
        public ActionResult<List<Models.OrderControllerModels.GetOrderResponse>> Get(string id)
        {
            var user = _unitOfWork.User.Get(u => u.Auth0Id == id);

            if (user == null)
                return NotFound();

            List<Order> orders = new List<Order>();
            if(user.Role == Role.NormalUser)
            {
                 orders = _unitOfWork.Order
                    .GetAll(o => o.Offer.Inquiry.User != null && o.Offer.Inquiry.User.Id == user.Id)
                    .ToList();
            }
            else if(user.Role == Role.OfficeWorker || user.Role == Role.Courier)
            {
                 orders = _unitOfWork.Order
                   .GetAll()
                   .ToList();
            }
            else if( user.Role == Role.Admin)
                orders = _unitOfWork.Order.GetAll().ToList();


            if (orders == null)
                return NoContent();

            List<Models.OrderControllerModels.GetOrderResponse> ordersResponse = new List<Models.OrderControllerModels.GetOrderResponse>();
            foreach (var order in orders)
                ordersResponse.Add(new Models.OrderControllerModels.GetOrderResponse(order));

            return Ok(ordersResponse);
        }

        [HttpGet("details/{id}")]
        public ActionResult<Models.OrderControllerModels.GetOrderResponse> Get(Guid id)
        {
            var order = _unitOfWork.Order.Get(order => order.Id == id);
            if (order == null)
                return NoContent();

            Models.OrderControllerModels.GetOrderResponse orderResponse = new Models.OrderControllerModels.GetOrderResponse(order);

            return Ok(orderResponse);
        }

        [Authorize(Policy = "ChangeStatus")]
        [HttpPut("details/{id}/changestatus")]
        public IActionResult ChangeOrderStatus(Guid id, [FromBody] OrderStatus orderStatus)
        {
            bool isStatusAccepted = true;
            if (Client.ActiveUser.Role == Role.OfficeWorker)
            {
                isStatusAccepted = orderStatus == OrderStatus.Pending || orderStatus == OrderStatus.Accepted || orderStatus == OrderStatus.Rejected;
            }
            else if (Client.ActiveUser.Role == Role.Courier)
            {
                isStatusAccepted = orderStatus == OrderStatus.Received || orderStatus == OrderStatus.Delivered || orderStatus == OrderStatus.CannotDeliver;
            }

            if (!isStatusAccepted)  // user nie ma uprawienien do danego statusu
                return NotFound();

            var order = _unitOfWork.Order.Get(o => o.Id == id);



            if (order == null)
                return NotFound();

            order.OrderStatus = orderStatus;
            _unitOfWork.Order.Update(order);
            _unitOfWork.Save();

            string mailSubject = "Order status has been changed";
            _emailSender.SendEmail(order.Sender.Email, EmailTemplates.CreateEmailForOrder(order.Id, order.OrderStatus, order.Sender.Name, order.Sender.Surname, false), mailSubject);
            _emailSender.SendEmail(order.Receiver.Email, EmailTemplates.CreateEmailForOrder(order.Id, order.OrderStatus, order.Receiver.Name, order.Receiver.Surname, false), mailSubject);

            return Ok(orderStatus);
        }
    }
}
