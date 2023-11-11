using Moq;
using PersonalFinance.Services;

namespace PersonalFinance.Tests
{
    public class CurrencyConverterTest
    {
        [Test]
        public void chekingTest()
        {
            /*string curr1 = "RUB";
            string curr2 = "USD";
            decimal amount = 100;
            decimal expected = 4600;

            IRateProvider rateProvider = new CsvRateProvider(new FileConnectorCSV().connector());

            CurrencyConverterCSV currency = new CurrencyConverterCSV(rateProvider);

            decimal actual = currency.Convert(curr1, curr2, amount);

            Assert.AreEqual(expected, actual);*/

            var mockRateProvider = new Mock<IRateProvider>();
            mockRateProvider.Setup(x => x.GetRate("WSD", "TWW")).Returns(46);

            var currencyConverter = new CurrencyConverterCSV(mockRateProvider.Object);

            decimal result = currencyConverter.Convert("WSD", "TWW", 100);

            Assert.AreEqual(4600, result);
        }
    }
    
}
