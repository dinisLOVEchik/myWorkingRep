using System;

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
        public RateResponse[] GetAll()
        {
            return _rateProvider.GetAll();
        }

        public string GetRateProviderSource()
        {
            return _rateProvider.GetType().Name;
        }
    }
}
