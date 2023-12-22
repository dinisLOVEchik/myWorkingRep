﻿using Moq;
using PersonalFinance.Services;

namespace PersonalFinance.Tests
{
    public class CurrencyConverterTests
    {
        [Test]
        public void ShouldCheckConvertMethodInCurrencyConverterCsvClass()
        {
            var mockRateProvider = new Mock<IRateProvider>();
            mockRateProvider.Setup(x => x.GetRate("USD", "EUR")).Returns(46);

            var currencyConverter = new CurrencyConverter(mockRateProvider.Object);

            decimal result = currencyConverter.Convert("USD", "EUR", 100);

            Assert.AreEqual(4600, result);
        }

        [Test]
        public void CsvCheckingTest()
        {
            IRateProvider provider = new CsvRateProvider("./data/Output.csv", ';', 30000);

            CurrencyConverter currencyConverter = new(provider);

            string curr1 = "RUB";
            string curr2 = "USD";
            decimal amount = 100;
            decimal expected = 4600;

            decimal actual = currencyConverter.Convert(curr1, curr2, amount);

            Assert.AreEqual(expected, actual);
        }
    }
}
