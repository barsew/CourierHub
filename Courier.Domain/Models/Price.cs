using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Courier.Domain.Models
{
    public class Price
    {
        public static Currency DefaultCurrency { get; } = Currency.PLN;
        [JsonIgnore]
        public int Id { get; set; }
        public Decimal Amount { get; set; }
        public Currency Currency { get; set; }
        public string Description { get; set; }
        [JsonIgnore]
        public Offer Offer { get; set; }
    }
}
