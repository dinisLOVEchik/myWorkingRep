using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinance.Tests
{
    internal class MockExchangeRatesGenerationService
    {
        const int MIN_VALUE = 1;
        const int MAX_VALUE = 1000;
        public List<ExcangeRate> MockExchangeRatesGenerator(List<string> isoCodesOfRates)
        {
            List<ExcangeRate> result = new List<ExcangeRate>();

            for (int currency1 = 0; currency1 < isoCodesOfRates.Count; currency1++)
            {
                for (int currency2 = 0; currency2 < isoCodesOfRates.Count; currency2++)
                {
                    CheckingPreliminaryListValuesBeforeAddingToFinalList(result, isoCodesOfRates, currency1, currency2);
                };
            };
            return result;
        }

        private void CheckingPreliminaryListValuesBeforeAddingToFinalList(List<ExcangeRate> result, List<string> isoCodesOfRates, int currency1, int currency2)
        {
            if (result.Count == 0 && !isoCodesOfRates[currency1].Equals(isoCodesOfRates[currency2]))
            {
                result.Add(new ExcangeRate(isoCodesOfRates[currency1], isoCodesOfRates[currency2], new Random().Next(MIN_VALUE, MAX_VALUE)));
            }
            else if (result.Count == 0)
            {
                result.Add(new ExcangeRate(isoCodesOfRates[currency1], isoCodesOfRates[currency2], MIN_VALUE));
            }
            else
            {
                if (isoCodesOfRates[currency1].Equals(isoCodesOfRates[currency2]))
                {
                    result.Add(new ExcangeRate(isoCodesOfRates[currency2], isoCodesOfRates[currency1], MIN_VALUE));
                }
                else
                {
                    AdditionalCheckingOfValuesBeforeAddingToFinalList(result, isoCodesOfRates, currency1, currency2);
                }

            }
        }
        private void AdditionalCheckingOfValuesBeforeAddingToFinalList (List<ExcangeRate> result, List<string> isoCodesOfRates, int currency1, int currency2)
        {
            int count = 0;
            foreach (ExcangeRate rate in result.ToList())
            {
                count++;
                if (isoCodesOfRates[currency1].Equals(rate.Currency2) && isoCodesOfRates[currency2].Equals(rate.Currency1))
                {
                    result.Add(new ExcangeRate(isoCodesOfRates[currency1], isoCodesOfRates[currency2], Math.Round(MIN_VALUE / rate.Rate, 7)));
                    break;
                }
                else if (count == result.Count)
                {
                    result.Add(new ExcangeRate(isoCodesOfRates[currency1], isoCodesOfRates[currency2], new Random().Next(MIN_VALUE, MAX_VALUE)));
                    break;
                }
            }
        }
    }

    public class ExcangeRate
    {
        public string Currency1 { get; }
        public string Currency2 { get; }
        public decimal Rate { get; }

        public ExcangeRate(string value1, string value2, decimal rate)
        {
            Currency1 = value1;
            Currency2 = value2;
            Rate = rate;
        }
    }
}
