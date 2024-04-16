using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courier.Domain.Models
{
    public class Company
    {
        public static Company OurCompany { get; } = new Company() {Id = 1, Name = "MLB"};
        public static Company TheOtherCompany { get; } = new Company() {Id = 2, Name = "TheOtherCompany"};
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
