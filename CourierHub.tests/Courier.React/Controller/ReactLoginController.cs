using Courier.Domain.Repository.IRepository;
using Courier.React;
using Courier.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Courier.React.Models.UserModel;
using System.Linq.Expressions;

namespace CourierHub.tests.Courier.React.Controller
{
    public class ReactLoginController
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReactLoginController()
        {
            _unitOfWork = A.Fake<IUnitOfWork>();
        }

        [Fact]
        public void LoginController_Post_CanUserLogin()
        {
            // Arrange
            Client.ActiveUser = null;
            var userInfo = new UserInfo()
            {
                sub = "test",
                role = "User"
            };
            User user = new User()
            {
                Subject = new Subject()
                {
                    Name = "test",
                    Surname = "testowy",
                    Phone = "888888888"
                }
            };
            A.CallTo(() => _unitOfWork.User.Get(A<Expression<Func<User, bool>>>.Ignored)).Returns(user);
            var controller = new ReactController.LoginController(_unitOfWork);

            // Act
            var result = controller.Post(userInfo);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkResult));
            Client.ActiveUser.Should().NotBeNull();
        }

        [Fact]
        public void LoginController_Post_CanUserRegister()
        {
            // Arrange
            Client.ActiveUser = null;
            var userInfo = new UserInfo()
            {
                sub = "test",
                role = "User",
                given_name = "test",
                family_name = "testowy",
                phone_number = "888888888"
            };
            A.CallTo(() => _unitOfWork.User.Get(A<Expression<Func<User, bool>>>.Ignored)).Returns(null);
            var controller = new ReactController.LoginController(_unitOfWork);

            // Act
            var result = controller.Post(userInfo);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkResult));
            Client.ActiveUser.Should().NotBeNull();
        }

        //private UserInfo MakeTestUserInfo()
        //{
        //    888
        //}

        [Fact]
        public void LoginController_Get_CanUserLogout()
        {
            // Arrange
            Client.ActiveUser = new User();
            var controller = new ReactController.LoginController(_unitOfWork);

            // Act
            var result = controller.Get();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkResult));
            Client.ActiveUser.Should().BeNull();
        }
    }
}
