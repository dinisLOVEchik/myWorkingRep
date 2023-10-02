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
            List<string> isoCodesOfRates = ParseHtmlWithZenRows.ParseHtmlWithZenRowsMethod();
            /*string pattern = @"[A-Z]{3}";
            foreach(string isoCode in ParseHtmlWithZenRows.ParseHtmlWithZenRowsMethod().ToList())
            {
                if (Regex.IsMatch(isoCode, pattern) && isoCode.Length == 3)
                {
                    isoCodesOfRates.Add(isoCode);
                }
            }*/

            MockExchangeRatesGenerationService service = new MockExchangeRatesGenerationService();

            List<ExcangeRate>rates = service.MockExchangeRatesGenerator(isoCodesOfRates);

            String _csvFile = @"Output.csv";

            String separator = ",";
            StringBuilder output = new StringBuilder();

            String[] headings = { "curr1", "curr2", "rate" };
            output.AppendLine(string.Join(separator, headings));

            foreach (ExcangeRate rate in rates.ToList())
            {
                String[] newLine = { rate.getValue1(), rate.getValue2(), rate.getRate().ToString() };
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
    }
}
