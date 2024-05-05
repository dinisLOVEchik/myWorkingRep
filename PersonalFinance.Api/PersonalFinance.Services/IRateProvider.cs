namespace PersonalFinance.Services
{
    public interface IRateProvider
    {
        decimal GetRate(string currencyFrom, string currencyTo);
        RateResponse[] GetAll();
    }
}
