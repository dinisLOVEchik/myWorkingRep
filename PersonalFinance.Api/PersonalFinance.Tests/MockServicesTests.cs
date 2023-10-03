using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PersonalFinance.Tests
{
    internal class MockServicesTests
    {
        [Test]
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

            List<ExcangeRate>rates = service.MockExchangeRatesGenerator(isoCodesOfRates);

            String _csvFile = @"Output.csv";

            String separator = ",";
            StringBuilder output = new StringBuilder();

            String[] headings = { "curr1", "curr2", "rate" };
            output.AppendLine(string.Join(separator, headings));

            foreach (ExcangeRate rate in rates.ToList())
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

        [Test]
        public void FirstTest()
        {
            List<string> currencies = new List<string> {"RUB", "USD", "EUR"};

            var service = new MockExchangeRatesGenerationService();

            List<ExcangeRate> rates = service.MockExchangeRatesGenerator(currencies);

            Assert.NotNull(rates);
            Assert.That(rates.Count, Is.EqualTo(9));
            Assert.That(rates[0].Rate, Is.EqualTo(1));
            Assert.IsTrue(rates[1].Rate > 1);
            Assert.IsTrue(rates[7].Rate < 1 && rates[7].Rate > 0);
        }
    }
}
