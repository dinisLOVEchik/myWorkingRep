using PersonalFinance.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace PersonalFinance.Tests
{
    internal class CurrencyConverterMSSQLTests
    {
        private readonly CurrencyConverterMSSQL _currencyConverter;

        public CurrencyConverterMSSQLTests()
        {
            _currencyConverter = new CurrencyConverterMSSQL();
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
