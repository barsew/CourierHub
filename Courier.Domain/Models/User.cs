using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courier.Domain.Models
{
    public class User
    {
        public int Id { get; set; }
        public Subject Subject { get; set; }
        public Role Role { get; set; } 
        public Company? Company { get; set; } 
        public string Auth0Id { get; set; }
    }
}
