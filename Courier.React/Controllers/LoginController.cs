using Courier.Domain.Repository.IRepository;
using Courier.React.Models.UserModel;
using Microsoft.AspNetCore.Mvc;

namespace Courier.React.Controllers
{
    [ApiController]
    [Route("api2/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public LoginController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public IActionResult Post(UserInfo userInfo)
        {
            var existingUser = _unitOfWork.User.Get(u => u.Auth0Id == userInfo.sub);
            if (existingUser == null) 
            { 
                var user = userInfo.MakeUser();

                _unitOfWork.User.Add(user);
                _unitOfWork.Save();
                Client.ActiveUser = user;
                existingUser = user;
            }
            else
            {
                if(userInfo.role != existingUser.Role.ToString())
                {
                    existingUser.Role = UserInfo.ConvertRole(userInfo.role);
                    _unitOfWork.User.Update(existingUser);
                    _unitOfWork.Save();
                }
                Client.ActiveUser = existingUser;
            }

            if (existingUser.Subject.Name == string.Empty || existingUser.Subject.Surname == string.Empty || existingUser.Subject.Phone == string.Empty)
            {
                var incompleteData = new
                {
                    name = existingUser.Subject.Name,
                    surname = existingUser.Subject.Surname,
                    phone = existingUser.Subject.Phone
                };

                return StatusCode(403, incompleteData);
            }
            else
            {
                return Ok();
            }
        }

        [HttpGet]
        public IActionResult Get()
        {
            Client.ActiveUser = null;
            return Ok();
        }
    }
}
