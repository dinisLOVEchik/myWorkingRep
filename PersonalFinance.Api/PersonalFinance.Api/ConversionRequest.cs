namespace PersonalFinance.Api
{
    public class ConversionRequest
    {
        public string CurrencyFrom { get; set; }
        public string CurrencyTo { get; set; }
        public string Amount { get; set; }

        public ConversionRequest(string currencyFrom, string currencyTo, string amount)
        {
            this.CurrencyFrom = currencyFrom;
            this.CurrencyTo = currencyTo;
            this.Amount = amount;
        }
    }
}
