using System.Text;

namespace PersonalFinance.Tests
{
    internal class MockServicesTests
    {
        [Test]
        [Ignore("Ignore a test")]
        public void RateGenerationTest()
        {
            var isoCodesOfRates = new List<string>
            {
                "AED", "AFN", "ALL", "AMD", "ANG", "AOA", "ARS", "AUD", "AWG", "AZN", "BAM", "BBD", "BDT", "BGN", "BHD",
                "BIF", "BMD", "BND", "BOB", "BOV", "BRL", "BSD", "BTN", "BWP", "BYN", "BZD", "CAD", "CDF", "CHE", "CHF",
                "CHW", "CLF", "CLP", "COP", "COU", "CRC", "CUP", "CVE", "CZK", "DJF", "DKK", "DOP", "DZD", "EGP", "ERN",
                "ETB", "EUR", "FJD", "FKP", "GBP", "GEL", "GHS", "GIP", "GMD", "GNF", "GTQ", "GYD", "HKD", "HNL", "HTG",
                "HUF", "IDR", "ILS", "INR", "IQD", "IRR", "ISK", "JMD", "JOD", "JPY", "KES", "KGS", "KHR", "KMF", "KPW",
                "KRW", "KWD", "KYD", "KZT", "LAK", "LBP", "LKR", "LRD", "LSL", "LYD", "MAD", "MDL", "MGA", "MKD", "MMK",
                "MNT", "MOP", "MRU", "MUR", "MVR", "MWK", "MXN", "MXV", "MYR", "MZN", "NAD", "NGN", "NIO", "NOK", "NPR",
                "NZD", "OMR", "PAB", "PEN", "PGK", "PHP", "PKR", "PLN", "PYG", "QAR", "RON", "RSD", "CNY", "RUB", "RWF",
                "SAR", "SBD", "SCR", "SDG", "SEK", "SGD", "SHP", "SLE", "SLL", "SOS", "SRD", "SSP", "STN", "SVC", "SYP",
                "SZL", "THB", "TJS", "TMT", "TND", "TOP", "TRY", "TTD", "TWD", "TZS", "UAH", "UGX", "USD", "USN", "UYI",
                "UYU", "UYW", "UZS", "VED", "VES", "VND", "VUV", "WST", "XAF", "XAG", "XAU", "XBA", "XBB", "XBC", "XBD",
                "XCD", "XDR", "XOF", "XPD", "XPF", "XPT", "XSU", "XTS", "XUA", "XXX", "YER", "ZAR", "ZMW", "ZWL"
            };

            var service = new MockExchangeRatesGenerationService();

            List<ExchangeRate> rates = service.MockExchangeRatesGenerator(isoCodesOfRates);

            String _csvFile = @"Output.csv";

            String separator = ",";
            StringBuilder output = new StringBuilder();

            String[] headings = { "curr1", "curr2", "rate" };
            output.AppendLine(string.Join(separator, headings));

            foreach (ExchangeRate rate in rates.ToList())
            {
                String[] newLine = { rate.Currency1, rate.Currency2, rate.Rate.ToString() };
                output.AppendLine(string.Join(separator, newLine));
            }

            try
            {
                File.AppendAllText(_csvFile, output.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Data could not be written to the CSV file.");
                return;
            }
        }

        private static readonly object[] _currencies = {
            new object[] { new List<string> { "RUB", "USD", "EUR" } }
        };

        [TestCase("RUB", "USD", "EUR")]
        public void IsTheListNotEmptyAndIsTheNumberOfCurrencyPairsCorrectTest(params string[] currencies)
        {
            var service = new MockExchangeRatesGenerationService();

            List<ExchangeRate> rates = service.MockExchangeRatesGenerator(currencies.ToList());

            Assert.NotNull(rates);
            Assert.That(rates.Count, Is.EqualTo(9));
        }

        [TestCase()]
        public void isEmptyListTest(params string[] currencies)
        {
            var service = new MockExchangeRatesGenerationService();

            List<ExchangeRate> rates = service.MockExchangeRatesGenerator(currencies.ToList());

            Assert.IsEmpty(rates);
        }

        [TestCase("RUB", "USD", "EUR")]
        public void IsTheExchangeRateToItselfEqualToOneTest(params string[] currencies)
        {
            var service = new MockExchangeRatesGenerationService();

            List<ExchangeRate> rates = service.MockExchangeRatesGenerator(currencies.ToList());

            Assert.That(rates[0].Rate, Is.EqualTo(1));
        }

        [TestCase("RUB", "USD", "EUR")]
        public void IsTheRateOfTheDominantCurrencyToTheSecondGreaterThanOneTest(params string[] currencies)
        {
            var service = new MockExchangeRatesGenerationService();

            List<ExchangeRate> rates = service.MockExchangeRatesGenerator(currencies.ToList());

            Assert.IsTrue(rates[1].Rate > 1);
        }

        [TestCase("RUB", "USD", "EUR")]
        public void IsTheRateOfTheFirstCurrencyToTheDominantLessThanOneTest(params string[] currencies)
        {
            var service = new MockExchangeRatesGenerationService();

            List<ExchangeRate> rates = service.MockExchangeRatesGenerator(currencies.ToList());

            Assert.IsTrue(rates[7].Rate < 1);
        }

        

        [TestCase("RUB", "USD", "EUR")]
        public void IsTheRateOfTheFirstCurrencyToTheDominantEqualToZeroTest(params string[] currencies)
        {
            var service = new MockExchangeRatesGenerationService();

            List<ExchangeRate> rates = service.MockExchangeRatesGenerator(currencies.ToList());

            Assert.IsTrue(rates[7].Rate > 0);
        }
    }
}
