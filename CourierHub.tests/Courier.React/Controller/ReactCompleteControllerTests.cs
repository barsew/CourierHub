using Courier.Domain.Models;
using Courier.Domain.Repository.IRepository;
using Courier.React.Models.UserModel;
using Courier.React;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CourierHub.tests.Courier.React.Controller
{
    public class ReactCompleteControllerTests
    {
        private readonly IUnitOfWork _unitOfWork;
        public ReactCompleteControllerTests()
        {
            _unitOfWork = A.Fake<IUnitOfWork>();
        }

        [Fact]
        public void CompleteController_Post_ReturnsOk()
        {
            var userInfo = new CompleteUserInfo();
            User user = new User()
            {
                Subject = new Subject()
            };
            A.CallTo(() => _unitOfWork.User.Get(A<Expression<Func<User, bool>>>.Ignored)).Returns(user);
            var controller = new ReactController.CompletedataController(_unitOfWork);

            // Act
            var result = controller.Post(userInfo);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkResult));
        }
    }
}
