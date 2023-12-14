using Moq;
using PersonalFinance.Services;
using Microsoft.Extensions.Configuration;

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
            IRateProvider provider = new CsvRateProvider("./data/Output.csv", ';');

            CurrencyConverter currencyConverter = new(provider);

            string curr1 = "RUB";
            string curr2 = "USD";
            decimal amount = 100;
            decimal expected = 4600;

            decimal actual = currencyConverter.Convert(curr1, curr2, amount);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void MySqlChekingTest()
        {
            IRateProvider provider = new MySqlRateProvider(GetConnectionString("MySQLConnection"));

            CurrencyConverter currencyConverter = new(provider);

            string curr1 = "RUB";
            string curr2 = "USD";
            decimal amount = 100;
            decimal expected = 4600;

            decimal actual = currencyConverter.Convert(curr1, curr2, amount);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void SqlChekingTest()
        {
            IRateProvider provider = new SqlServerRateProvider(GetConnectionString("MSSQLConnection"));

            CurrencyConverter currencyConverter = new(provider);

            string curr1 = "RUB";
            string curr2 = "USD";
            decimal amount = 100;
            decimal expected = 4600;

            decimal actual = currencyConverter.Convert(curr1, curr2, amount);

            Assert.AreEqual(expected, actual);
        }

        private string GetConnectionString(string serverName)
        {
            var builder = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfiguration _configuration = builder.Build();

            var connectionString = _configuration.GetConnectionString(serverName);

            return connectionString;
        }
    }
}
