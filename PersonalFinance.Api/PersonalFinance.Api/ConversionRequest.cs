using System.Text.Json.Serialization;

namespace PersonalFinance.Api
{
    public class ConversionRequest
    {
        [JsonPropertyName("currency_from")]
        public string CurrencyFrom { get; set; }
        [JsonPropertyName("currency_to")]
        public string CurrencyTo { get; set; }
        [JsonPropertyName("amount")]
        public decimal Amount { get; set; }
        [JsonPropertyName("rates_source")]
        public string FxRatesSource { get; set; }

        public ConversionRequest(string currencyFrom, string currencyTo, decimal amount, string fxRatesSource)
        {
            this.CurrencyFrom = currencyFrom.ToUpper();
            this.CurrencyTo = currencyTo.ToUpper();
            this.Amount = amount;
            this.FxRatesSource = fxRatesSource;
        }
    }
}
