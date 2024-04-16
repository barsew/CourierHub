using Courier.DataAccess.Repository;
using Courier.Domain.Repository.IRepository;
using Courier.React.Models.UserModel;
using Microsoft.AspNetCore.Mvc;

namespace Courier.React.Controllers
{
    [ApiController]
    [Route("api2/[controller]")]
    public class CompletedataController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CompletedataController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public IActionResult Post(CompleteUserInfo userInfo)
        {
            var existingUser = _unitOfWork.User.Get(u => u.Auth0Id == userInfo.Auth0Id);
            if(existingUser != null)
            {
                existingUser.Subject.Name = userInfo.Name;
                existingUser.Subject.Surname = userInfo.Surname;
                existingUser.Subject.Phone = userInfo.Phone;
                _unitOfWork.User.Update(existingUser);
                _unitOfWork.Save();

                if(Client.ActiveUser != null)
                    Client.ActiveUser = existingUser;
            }
            else
            {
                return StatusCode(403, new { message = "User to update not found" });
            }
            return Ok();
        }

        [HttpGet]
        public ActionResult<UserInfo> Get()
        {
            if(Client.ActiveUser?.Subject != null)
            {
                UserDataResponse response = new UserDataResponse();
                response.Name = Client.ActiveUser.Subject.Name;
                response.Surname = Client.ActiveUser.Subject.Surname;
                response.Phone = Client.ActiveUser.Subject.Phone;
                response.Email = Client.ActiveUser.Subject.Email;
                return Ok(response);
            }
            return Ok();
        }

    }
}
