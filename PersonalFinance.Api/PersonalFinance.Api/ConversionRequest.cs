namespace PersonalFinance.Api
{
    public class ConversionRequest
    {
        public string CurrencyFrom { get; set; }
        public string CurrencyTo { get; set; }
        public string Amount { get; set; }
        public string Source { get; set; }

        public ConversionRequest(string currencyFrom, string currencyTo, string amount, string source)
        {
            this.CurrencyFrom = currencyFrom;
            this.CurrencyTo = currencyTo;
            this.Amount = amount;
            this.Source = source;
        }
    }
}
