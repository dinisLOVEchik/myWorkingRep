using System.Text.Json.Serialization;

namespace PersonalFinance.Services
{
    public class CurrencyExchangeRate
    {
        [JsonPropertyName("from")]
        public string From { get; set; }
        [JsonPropertyName("to")]
        public string To { get; set; }
        [JsonPropertyName("rate")]
        public string Rate { get; set; }

        public CurrencyExchangeRate(string from, string to, string rate)
        {
            this.From = from.ToUpper();
            this.To = to.ToUpper();
            this.Rate = rate;
        }
    }
}
