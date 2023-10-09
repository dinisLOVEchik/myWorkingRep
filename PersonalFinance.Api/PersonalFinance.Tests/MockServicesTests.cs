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

            var rates = service.MockExchangeRatesGenerator(isoCodesOfRates);

            var separator = ",";
            StringBuilder output = new StringBuilder();

            string[] headings = { "curr1", "curr2", "rate" };
            output.AppendLine(string.Join(separator, headings));

            foreach (ExchangeRate rate in rates.ToList())
            {
                string[] newLine = { rate.Currency1, rate.Currency2, rate.Rate.ToString() };
                output.AppendLine(string.Join(separator, newLine));
            }

            try
            {
                File.AppendAllText("Output.csv", output.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Data could not be written to the CSV file.");
                return;
            }
        }

        [Test]
        public void IsTheListNotEmptyAndIsTheNumberOfCurrencyPairsCorrectTest()
        {
            List<string> currencies = new () { "RUB", "USD", "EUR" };

            var service = new MockExchangeRatesGenerationService();

            var rates = service.MockExchangeRatesGenerator(currencies);

            Assert.NotNull(rates);
            Assert.That(rates.Count, Is.EqualTo(9));
        }

        [Test]
        public void IsTheExchangeRateToItselfEqualToOneTest()
        {
            List<string> currencies = new () { "RUB", "USD", "EUR" };

            var service = new MockExchangeRatesGenerationService();

            var rates = service.MockExchangeRatesGenerator(currencies);

            Assert.That(rates[0].Rate, Is.EqualTo(1));
        }

        [Test]
        public void IsTheRateOfTheDominantCurrencyToTheSecondGreaterThanOneTest()
        {
            List<string> currencies = new () { "RUB", "USD", "EUR" };

            var service = new MockExchangeRatesGenerationService();

            var rates = service.MockExchangeRatesGenerator(currencies);

            Assert.That(rates[1].Rate, Is.GreaterThan(1));
        }

        [Test]
        public void IsTheRateOfTheFirstCurrencyToTheDominantLessThanOneTest()
        {
            List<string> currencies = new () { "RUB", "USD", "EUR" };

            var service = new MockExchangeRatesGenerationService();

            var rates = service.MockExchangeRatesGenerator(currencies);

            Assert.That(rates[7].Rate, Is.LessThan(1));
        }

        [Test]
        public void IsTheRateOfTheFirstCurrencyToTheDominantEqualToZeroTest()
        {
            List<string> currencies = new () { "RUB", "USD", "EUR" };

            var service = new MockExchangeRatesGenerationService();

            var rates = service.MockExchangeRatesGenerator(currencies);

            Assert.That(rates[7].Rate, Is.GreaterThan(0));
        }
    }
}
