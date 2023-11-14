namespace PersonalFinance.Services
{
    public class CurrencyConverter
    {
        private readonly IRateProvider rateProvider;

        public CurrencyConverter(IRateProvider rateProvider)
        {
            this.rateProvider = rateProvider;
        }

        public decimal Convert(string currencyFrom, string currencyTo, decimal amount)
        {
            var rate = rateProvider.GetRate(currencyFrom, currencyTo);
            return rate * amount;
        }
    }
    
}
