using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PersonalFinance.Tests
{
    internal class MockExchangeRatesGenerationService
    {
        const int MIN_VALUE = 1;
        const int MAX_VALUE = 1000;
        public List<ExchangeRate> MockExchangeRatesGenerator(List<string> isoCodesOfRates)
        {
            List<ExchangeRate> result = new List<ExchangeRate>();

            for (int currency1 = 0; currency1 < isoCodesOfRates.Count; currency1++)
            {
                for (int currency2 = 0; currency2 < isoCodesOfRates.Count; currency2++)
                {
                    CheckAndAddingInputCoupleIfListIsEmpty(result, isoCodesOfRates, currency1, currency2);
                };
            };
            return result;
        }

        private void CheckAndAddingInputCoupleIfListIsEmpty(List<ExchangeRate> result, List<string> isoCodesOfRates, int currency1, int currency2)
        {
            if (result.Count == 0 && !isoCodesOfRates[currency1].Equals(isoCodesOfRates[currency2]))
            {
                result.Add(new ExchangeRate(isoCodesOfRates[currency1], isoCodesOfRates[currency2], new Random().Next(MIN_VALUE, MAX_VALUE)));
            }
            else if (result.Count == 0)
            {
                result.Add(new ExchangeRate(isoCodesOfRates[currency1], isoCodesOfRates[currency2], MIN_VALUE));
            }
            else
            {
                IfInputCoupleIsTwinsAndListIsEmptyAddCoupleToList(result, isoCodesOfRates, currency1, currency2);
            }
        }

        private void IfInputCoupleIsTwinsAndListIsEmptyAddCoupleToList(List<ExchangeRate> result, List<string> isoCodesOfRates, int currency1, int currency2)
        {
            if (isoCodesOfRates[currency1].Equals(isoCodesOfRates[currency2]))
            {
                result.Add(new ExchangeRate(isoCodesOfRates[currency2], isoCodesOfRates[currency1], MIN_VALUE));
            }
            else
            {
                IterateOverListProvidedThatInputPairIsNotTwins(result, isoCodesOfRates, currency1, currency2);
            }
        }
        private void IterateOverListProvidedThatInputPairIsNotTwins (List<ExchangeRate> result, List<string> isoCodesOfRates, int currency1, int currency2)
        {
            int count = 0;
            foreach (ExchangeRate rate in result.ToList())
            {
                count++;
                AddIfThereIsMirrorCoupleOfInputPairAndAlsoAddInputPairIfItIsntInLlist(result, isoCodesOfRates, currency1, currency2, rate, count);
            }
        }

        private void AddIfThereIsMirrorCoupleOfInputPairAndAlsoAddInputPairIfItIsntInLlist(List<ExchangeRate> result, List<string> isoCodesOfRates, int currency1, int currency2, ExchangeRate rate, int count)
        {
            if (isoCodesOfRates[currency1].Equals(rate.Currency2) && isoCodesOfRates[currency2].Equals(rate.Currency1))
            {
                result.Add(new ExchangeRate(isoCodesOfRates[currency1], isoCodesOfRates[currency2], Math.Round(MIN_VALUE / rate.Rate, 7)));
            }
            else if (count == result.Count)
            {
                result.Add(new ExchangeRate(isoCodesOfRates[currency1], isoCodesOfRates[currency2], new Random().Next(MIN_VALUE, MAX_VALUE)));
            }
        }
    }
}
