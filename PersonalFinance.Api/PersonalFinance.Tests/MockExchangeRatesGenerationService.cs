namespace PersonalFinance.Tests
{
    internal class MockExchangeRatesGenerationService
    {
        const int MIN_VALUE = 1;
        const int MAX_VALUE = 1000;
        private readonly Random _random = new ();
        public List<ExchangeRate> MockExchangeRatesGenerator(List<string> isoCodesOfRates)
        {
            var result = new List<ExchangeRate>();

            foreach (var currency1 in isoCodesOfRates)
            {
                foreach (var currency2 in isoCodesOfRates)
                {
                    AddPairOfCurrencies(result, currency1, currency2);
                };
            };
            return result;
        }
        private void AddPairOfCurrencies (List<ExchangeRate> result, string currency1, string currency2)
        {
            if (currency1 == currency2)
            {
                result.Add(new ExchangeRate(currency1, currency2, MIN_VALUE));
            }
            else
            {
                var pair = result.SingleOrDefault(x => x.Currency1 == currency2 && x.Currency2 == currency1);
                result.Add(pair != null
                    ? new ExchangeRate(currency1, currency2, Math.Round(MIN_VALUE / pair.Rate, 7))
                    : new ExchangeRate(currency1, currency2, _random.Next(MIN_VALUE, MAX_VALUE)));
            }
        }
    }
}
