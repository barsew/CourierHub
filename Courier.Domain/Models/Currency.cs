using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Courier.Domain.Models
{
    public enum Currency
    {
        PLN = 0,
        USD = 1,
        EUR = 2,
        CHF = 3
    }
}
