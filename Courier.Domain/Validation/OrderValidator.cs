using Courier.Domain.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courier.Domain.Validation
{
    public class OrderValidator : AbstractValidator<Order>
    { 
        public OrderValidator() 
        {
            RuleFor(o => o.Sender).SetValidator(new SubjectValidator());
        }
    }
}
