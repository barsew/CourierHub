using Courier.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Courier.Domain.Services.Email
{
    public class EmailSender : IEmailSender
    {
        private readonly string senderEmail = "courier.hub.blm@gmail.com";
        private readonly string senderPassword = "INVALID_PASSWORD";
        private readonly int smtpPort = 587;
        public void SendEmail(string recipientEmail, string body, string subject)
        {
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", smtpPort)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(senderEmail, senderPassword)
            };

            var message = new MailMessage
            {
                From = new MailAddress(senderEmail),
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
            };

            message.To.Add(recipientEmail);

            try
            {
                smtpClient.Send(message);
            }
            catch (Exception ex)
            {
                // TODO: logi
            }
        }

      
    }
}
