using Courier.Domain.Models;
using Courier.Domain.Repository.IRepository;
using Courier.Domain.Services.OfferService;
using Courier.Domain.Validation;
using CourierAPI.Models;
using FluentAssertions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourierHub.tests.CourierAPI
{
    public class ApiInquiryControllerTests
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOfferService _offerService;
        private readonly IValidator<Inquiry> _validator;
        public ApiInquiryControllerTests()
        {
            _unitOfWork = A.Fake<IUnitOfWork>();
            _offerService = new OfferService();
            _validator = new InquiryValidator();
        }

        [Fact]
        public void InquiryController_PostInquiry_ReturnsOk()
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
            var controller = new ApiController.InquiryController(_unitOfWork, _offerService, _validator);

            // Act
            var response = controller.PostInquiry(inquiryRequest);

            // Assert
            response.Should().NotBeNull();
            var result = response.Result;
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
            ((OkObjectResult)result).Value.Should().NotBeNull();
            var offerResponse = (OfferResponse)((OkObjectResult)result).Value;
            offerResponse.Should().NotBeNull();
            offerResponse.FullPrice.Should().Match((Price fp) => fp.Amount > 0);
        }

        [Fact]
        public void InquiryController_PostInquiry_ReturnsBadRequest()
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
            var controller = new ApiController.InquiryController(_unitOfWork, _offerService, _validator);

            // Act
            var response = controller.PostInquiry(inquiryRequest);

            // Assert
            response.Should().NotBeNull();
            var result = response.Result;
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(BadRequestObjectResult));
        }
    }
}
