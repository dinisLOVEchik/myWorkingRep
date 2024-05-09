namespace PersonalFinance.Services
{
    public interface IRateProvider
    {
        decimal GetRate(string currencyFrom, string currencyTo);
        CurrencyExchangeRate[] GetAll();
        bool IsAvailable();
    }
}
