using Courier.Domain.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courier.Domain.Validation
{
    public class InquiryValidator : AbstractValidator<Inquiry>
    {
        public InquiryValidator() 
        {
            RuleFor(i => i.Weight).GreaterThan(0);
            RuleFor(i => i.Width).GreaterThan(0);
            RuleFor(i => i.Length).GreaterThan(0);
            RuleFor(i => i.Height).GreaterThan(0);
            RuleFor(i => i.PickupDate).Must(d => d > DateTime.Now).WithMessage("Pickup date must be in future");
            RuleFor(i => new {i.PickupDate, i.DeliveryDate}).
                Must(o => o.PickupDate < o.DeliveryDate).WithMessage("Delivery date must be grater than Pickup date");
        }
    }
}
