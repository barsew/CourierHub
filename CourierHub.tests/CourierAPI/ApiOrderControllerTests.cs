using Courier.Domain.Services.Email;
using Courier.Domain.Models;
using Courier.Domain.Repository.IRepository;
using Courier.Domain.Services.OfferService;
using Courier.Domain.Services.OrderService;
using Courier.Domain.Validation;
using FluentValidation;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using Azure.Core;

namespace CourierHub.tests.CourierAPI
{
    public class ApiOrderControllerTests
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOrderService _orderService;
        private readonly IValidator<Order> _validator;
        private readonly IEmailSender _emailSender;

        public ApiOrderControllerTests()
        {
            _unitOfWork = A.Fake<IUnitOfWork>();
            _orderService = new OrderService();
            _validator = new OrderValidator();
            _emailSender = A.Fake<IEmailSender>();
        }

        [Fact]
        public void OrderController_Post_ReturnsOk()
        {
            // Assert
            var orderRequest = new ApiModels.OrderControllerModels.OrderRequest()
            {
                OfferId = new Guid(),
                SenderName = "Sender",
                SenderSurname = "Sending",
                SenderEmail = "sender@test.com",
                SenderPhone = "888888888",
                ReceiverName = "Receiver",
                ReceiverSurname = "Receiving",
                ReceiverEmail = "receiver@test.com",
                ReceiverPhone = "555555555"
            };
            SetupPostMock();
            var controller = new ApiController.OrderController(_unitOfWork, _orderService, _validator, _emailSender);

            // Act
            var response = controller.Post(orderRequest);

            // Assert
            response.Should().NotBeNull();
            var result = response.Result;
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Fact]
        public void OrderController_Post_ReturnsBadRequest()
        {
            // Arrange
            var orderRequest = new ApiModels.OrderControllerModels.OrderRequest()
            {
                OfferId = new Guid(),
                SenderName = "Sender",
                SenderSurname = "Sending",
                SenderEmail = "invalidMail",
                SenderPhone = "invalidPhone",
                ReceiverName = "Receiver",
                ReceiverSurname = "Receiving",
                ReceiverEmail = "receiver@test.com",
                ReceiverPhone = "555555555"
            };
            SetupPostMock();
            var controller = new ApiController.OrderController(_unitOfWork, _orderService, _validator, _emailSender);

            // Act
            var response = controller.Post(orderRequest);

            // Assert
            response.Should().NotBeNull();
            var result = response.Result;
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(BadRequestObjectResult));
        }

        private void SetupPostMock()
        {
            var offer = new Offer();
            offer.ExpireDate = DateTime.Now.AddDays(1);
            A.CallTo(() => _unitOfWork.Offer.Get(A<Expression<Func<Offer, bool>>>.Ignored)).Returns(offer);
        }

        [Fact]
        public void OrderController_Get_ReturnsOk()
        {
            // Arrange
            var guid = new Guid();
            var order = new Order()
            {
                Offer = new Offer()
                {
                    Inquiry = new Inquiry(),
                    PriceBreakdown = new Price[]
                    {
                        new Price() {Amount = 10, Currency = Currency.PLN},
                        new Price() {Amount = 12, Currency = Currency.PLN},
                    }
                },
                Sender = new Subject(),
                Receiver = new Subject()
            };
            A.CallTo(() => _unitOfWork.Order.Get(A<Expression<Func<Order, bool>>>.Ignored)).Returns(order);
            var controller = new ApiController.OrderController(_unitOfWork, _orderService, _validator, _emailSender);

            // Act
            var response = controller.Get(guid);
            
            // Assert
            response.Should().NotBeNull();
            var result = response.Result;
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }
    }
}
