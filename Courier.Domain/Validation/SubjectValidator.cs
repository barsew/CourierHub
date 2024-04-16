using Courier.Domain.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courier.Domain.Validation
{
    public class SubjectValidator : AbstractValidator<Subject>
    {
        public SubjectValidator() 
        { 
            RuleFor(s => s.Name).NotEmpty();
            RuleFor(s => s.Surname).NotEmpty();
            RuleFor(s => s.Email).EmailAddress();
            RuleFor(s => s.Phone).Length(9);
        }
    }
}
