using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinance.Tests
{
    public class ExchangeRate
    {
        public string Currency1 { get; }
        public string Currency2 { get; }
        public decimal Rate { get; }

        public ExchangeRate(string value1, string value2, decimal rate)
        {
            Currency1 = value1;
            Currency2 = value2;
            Rate = rate;
        }
    }
}
