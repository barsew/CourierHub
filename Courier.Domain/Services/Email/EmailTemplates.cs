using Courier.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courier.Domain.Services.Email
{
    public class EmailTemplates
    {
        public static string CreateEmailForOrder(Guid orderNumber, OrderStatus status, string name, string surname, bool isOrderJustCreated)
        {
            string bodyTemplate = @"<!DOCTYPE html>
                            <html lang=""en"">
                            <head>
                                <meta charset=""UTF-8"">
                                <meta http-equiv=""X-UA-Compatible"" content=""IE=edge"">
                                <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                                <title>Email Template</title>
                            </head>
                            <body>
                                <p>Dear {name} {surname},</p>

                                {orderActionContent}

                                <p>Order Number: {orderNumber}</p>
                                <p>Status: {status}</p>

                                <p>Thank you for choosing our service.</p>

                                <p>Track your order <a href=""{orderTrackingLink}"">here</a>.</p>
                            </body>
                            </html>";

            // Zastąpienie placeholderów rzeczywistymi wartościami
            string orderActionContent = isOrderJustCreated
                ? "<p>Your order has been successfully created.</p>"
                : "<p>The status of your order has been updated.</p>";

            // Tworzenie linku do śledzenia zamówienia
            string orderTrackingLink = $"https://localhost:44456/tracking/{orderNumber}"; // TODO: do zmiany

            bodyTemplate = bodyTemplate.Replace("{name}", name)
                                       .Replace("{surname}", surname)
                                       .Replace("{orderNumber}", orderNumber.ToString())
                                       .Replace("{status}", status.ToString())
                                       .Replace("{orderActionContent}", orderActionContent)
                                       .Replace("{orderTrackingLink}", orderTrackingLink);

            return bodyTemplate;
        }
    }
}
