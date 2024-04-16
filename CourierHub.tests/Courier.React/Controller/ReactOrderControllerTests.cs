using Courier.Domain.Models;
using Courier.Domain.Repository.IRepository;
using Courier.Domain.Services.Email;
using Courier.Domain.Services.OrderService;
using Courier.Domain.Validation;
using Courier.React;
using CourierAPI.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq.Expressions;

namespace CourierHub.tests.Courier.React.Controller
{
    public class ReactOrderControllerTests
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOrderService _orderService;
        private readonly IValidator<Order> _validator;
        private readonly IEmailSender _emailSender;

        public ReactOrderControllerTests()
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
            var orderRequest = new ReactModels.OrderControllerModels.OrderRequest()
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
            Client.ActiveUser = null;
            SetupPostMock();
            var controller = new ReactController.OrderController(_unitOfWork, _orderService, _validator, _emailSender);

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
            var orderRequest = new ReactModels.OrderControllerModels.OrderRequest()
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
            Client.ActiveUser = null;
            SetupPostMock();
            var controller = new ReactController.OrderController(_unitOfWork, _orderService, _validator, _emailSender);

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
        public void OrderController_GetByGuid_ReturnsOk()
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
            var controller = new ReactController.OrderController(_unitOfWork, _orderService, _validator, _emailSender);

            // Act
            var response = controller.Get(guid);

            // Assert
            response.Should().NotBeNull();
            var result = response.Result;
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Fact]
        public void OrderController_GetByUser_ReturnsOk()
        {
            // Arrange
            var id = "testId";
            var orders = new Order[]
            {
                new Order()
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
                }
            };
            A.CallTo(() => _unitOfWork.Order.GetAll(A<Expression<Func<Order, bool>>>.Ignored)).Returns(orders);
            var controller = new ReactController.OrderController(_unitOfWork, _orderService, _validator, _emailSender);

            // Act
            var response = controller.Get(id);

            // Assert
            response.Should().NotBeNull();
            var result = response.Result;
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Fact]
        public void OrderController_ChangeOrderStatus_OfficeWorkerCanAccept()
        {
            // Arrange
            var guid = new Guid();
            Client.ActiveUser = new User()
            {
                Role = Role.OfficeWorker
            };
            SetupChangeStatusMock();
            var controller = new ReactController.OrderController(_unitOfWork, _orderService, _validator, _emailSender);

            // Act
            var result = controller.ChangeOrderStatus(guid, OrderStatus.Accepted);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
            var orderStatus = (OrderStatus)((OkObjectResult)result).Value;
            orderStatus.Should().Be(OrderStatus.Accepted);
        }

        [Fact]
        public void OrderController_ChangeOrderStatus_CourierCanDeliver()
        {
            // Arrange
            var guid = new Guid();
            Client.ActiveUser = new User()
            {
                Role = Role.Courier
            };
            SetupChangeStatusMock();
            var controller = new ReactController.OrderController(_unitOfWork, _orderService, _validator, _emailSender);

            // Act
            var result = controller.ChangeOrderStatus(guid, OrderStatus.Delivered);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
            var orderStatus = (OrderStatus)((OkObjectResult)result).Value;
            orderStatus.Should().Be(OrderStatus.Delivered);
        }
        private void SetupChangeStatusMock()
        {
            var order = new Order()
            {
                Sender = new Subject(),
                Receiver = new Subject()
            };
            A.CallTo(() => _unitOfWork.Order.Get(A<Expression<Func<Order, bool>>>.Ignored)).Returns(order);
        }
    }
}
