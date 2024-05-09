namespace PersonalFinance.Services
{
    public class CurrencyExchangeRateRepository
    {
        private readonly IRateProvider _rateProvider;
        public CurrencyExchangeRateRepository(IRateProvider rateProvider)
        {
            this._rateProvider = rateProvider;
        }
        public CurrencyExchangeRate[] GetAll()
        {
            return _rateProvider.GetAll();
        }
    }
}
