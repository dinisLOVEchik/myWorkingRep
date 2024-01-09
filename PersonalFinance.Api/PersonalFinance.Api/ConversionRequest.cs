namespace PersonalFinance.Api
{
    public class ConversionRequest
    {
        public string CurrencyFrom { get; set; }
        public string CurrencyTo { get; set; }
        public int Amount { get; set; }

        public ConversionRequest(string currencyFrom, string currencyTo, int amount)
        {
            this.CurrencyFrom = currencyFrom;
            this.CurrencyTo = currencyTo;
            this.Amount = amount;
        }
    }
}
