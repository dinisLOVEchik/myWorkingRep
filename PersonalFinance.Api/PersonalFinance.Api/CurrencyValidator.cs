using System.Text.RegularExpressions;

namespace PersonalFinance.Api
{
    public class CurrencyValidator
    {
        private readonly string _currencyPattern = "^[A-Z]{3}$";

        public bool ValidateRequest(string from, string to, string am)
        {
            return Regex.IsMatch(from, _currencyPattern) && Regex.IsMatch(to, _currencyPattern)
                && int.TryParse(am, out int amount);
        }
    }
}
