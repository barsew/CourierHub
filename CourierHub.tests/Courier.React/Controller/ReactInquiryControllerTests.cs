using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Courier.Domain.Models;
using Courier.Domain.Repository.IRepository;
using Courier.Domain.Services.OfferService;
using Courier.Domain.Validation;
using Courier.React.Controllers;
using CourierAPI.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace UnitTesting.Courier.React.Controller
{
    public class ReactInquiryControllerTests
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOfferService _offerService;
        private readonly IValidator<Inquiry> _validator;
        public ReactInquiryControllerTests()
        {
            _unitOfWork = A.Fake<IUnitOfWork>();
            _offerService = new OfferService();
            _validator = new InquiryValidator();
        }

        [Fact]
        public void InquiryController_Get_ReturnsOk()
        {
            // Arrange
            var controller = new ReactController.InquiryController(_unitOfWork, _offerService, _validator);

            // Act
            var response = controller.Get();

            // Assert
            response.Should().NotBeNull();
            var result = response.Result;
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Fact]
        public void InquiryController_GetWithId_ReturnsOk()
        {
            // Arrange
            string id = "testId";
            var controller = new ReactController.InquiryController(_unitOfWork, _offerService, _validator);

            // Actt
            var response = controller.Get(id);

            // Assert
            response.Should().NotBeNull();
            var result = response.Result;
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Fact]
        public void InquiryController_Post_ReturnsOk()
        {
            // Arrange
            var inquiryRequest = new InquiryRequest()
            {
                Weight = 3,
                Width = 10,
                Length = 5,
                Height = 8,
                SourceAddress = new Address()
                {
                    HouseNumber = "75",
                    Street = "Koszykowa",
                    City = "Warszawa",
                    ZipCode = "00-003",
                    Country = "Polska"
                },
                DestinationAddress = new Address()
                {
                    HouseNumber = "17",
                    Street = "Szkolna",
                    City = "Białystok",
                    ZipCode = "56-120",
                    Country = "Polska"
                },
                DeliveryDate = DateTime.Now.AddDays(10),
                PickupDate = DateTime.Now.AddDays(5),
                IsCompany = true,
                IsPriorityHigh = true,
                DeliveryAtWeekend = true
            };
            var controller = new ReactController.InquiryController(_unitOfWork, _offerService, _validator);

            // Act
            var response = controller.Post(inquiryRequest);

            // Assert
            response.Should().NotBeNull();
            var result = response.Result;
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
            ((OkObjectResult)result).Value.Should().NotBeNull();
            var offerResponse = (OfferResponse) ((OkObjectResult)result).Value;
            offerResponse.Should().NotBeNull();
            offerResponse.FullPrice.Should().Match((Price fp) => fp.Amount > 0);
        }

        [Fact]
        public void InquiryController_Post_ReturnsBadRequest()
        {
            var inquiryRequest = new InquiryRequest()
            {
                Weight = 3,
                Width = 10,
                Length = 5,
                Height = 8,
                SourceAddress = new Address()
                {
                    HouseNumber = "75",
                    Street = "Koszykowa",
                    City = "Warszawa",
                    ZipCode = "00-003",
                    Country = "Polska"
                },
                DestinationAddress = new Address()
                {
                    HouseNumber = "17",
                    Street = "Szkolna",
                    City = "Białystok",
                    ZipCode = "56-120",
                    Country = "Polska"
                },
                DeliveryDate = DateTime.Now.AddDays(10),
                PickupDate = DateTime.Now.AddDays(11),
                IsCompany = true,
                IsPriorityHigh = true,
                DeliveryAtWeekend = true
            };
            var controller = new ReactController.InquiryController(_unitOfWork, _offerService, _validator);

            // Act
            var response = controller.Post(inquiryRequest);

            // Assert
            response.Should().NotBeNull();
            var result = response.Result;
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(BadRequestObjectResult));
        }
    }
}
