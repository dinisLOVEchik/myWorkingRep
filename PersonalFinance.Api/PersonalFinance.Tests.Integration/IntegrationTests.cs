using PersonalFinance.Services;
using Microsoft.Extensions.Configuration;

namespace PersonalFinance.Tests.Integration
{
    public class IntegrationTests
    {
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
        public void SqlServerChekingTest()
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