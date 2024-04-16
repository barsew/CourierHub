using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courier.Domain.Services.Email
{
    public interface IEmailSender
    {
        void SendEmail(string recipientEmail, string body, string subject);
    }
}
