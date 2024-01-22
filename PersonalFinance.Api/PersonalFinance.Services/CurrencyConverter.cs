namespace PersonalFinance.Services
{
    public class CurrencyConverter
    {
        private readonly IRateProvider _rateProvider;

        public CurrencyConverter(IRateProvider rateProvider)
        {
            this._rateProvider = rateProvider;
        }

        public decimal Convert(string currencyFrom, string currencyTo, decimal amount)
        {
            var rate = _rateProvider.GetRate(currencyFrom, currencyTo);
            return rate * amount;
        }

        public string GetRateProviderSource(string source)
        {
            if (source == )
            return _rateProvider.GetType().Name;
        }
    }
    
}
