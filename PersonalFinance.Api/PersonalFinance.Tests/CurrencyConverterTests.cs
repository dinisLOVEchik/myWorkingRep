using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersonalFinance.Services;

namespace PersonalFinance.Tests
{
    public class CurrencyConverterTest
    {
        private readonly CurrencyConverter _currencyConverter;

        public CurrencyConverterTest()
        {
            _currencyConverter = new CurrencyConverter();
        }

        [Test]
        public void chekingTest()
        {
            string curr1 = "RUB";
            string curr2 = "USD";
            decimal amount = 100;
            decimal expected = 4600;

            decimal actual = _currencyConverter.Convert(curr1, curr2, amount);

            Assert.AreEqual(expected, actual);

        }
    }
    
}
