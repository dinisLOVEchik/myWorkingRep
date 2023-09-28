using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinance.Tests
{
    internal class MockServicesTests
    {
        public void RateGenerationTest()
        {
            List<string> isoCodesOfRates = new List<string>();
            isoCodesOfRates.Add("USD");
            isoCodesOfRates.Add("RUB");
            isoCodesOfRates.Add("EUR");
            isoCodesOfRates.Add("AZN");
            isoCodesOfRates.Add("BRL");
            isoCodesOfRates.Add("BYN");
            isoCodesOfRates.Add("CAD");
            isoCodesOfRates.Add("CHF");
            isoCodesOfRates.Add("SAR");

            MockExchangeRatesGenerationService service = new MockExchangeRatesGenerationService();

            List<ExcangeRate>rates = service.MockExchangeRatesGenerator(isoCodesOfRates);

            String _csvFile = @"G:/СиШарп/myWorkingRep/PersonalFinance.Api/Output.csv";

            String separator = ",";
            StringBuilder output = new StringBuilder();

            String[] headings = { "First currency", "Second currency", "Exchange Rate" };
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
