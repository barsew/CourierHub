using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Courier.Domain.Models
{
    public class Address
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string HouseNumber { get; set; }
        public string? ApartmentNumber { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
    }
}
