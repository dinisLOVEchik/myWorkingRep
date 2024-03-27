using System.Text.RegularExpressions;

namespace PersonalFinance.Api
{
    public class CurrencyValidator
    {
        private readonly string _currencyPattern = "^[A-Z]{3}$";

        public bool ValidateRequest(string from, string to, decimal am)
        {
            return Regex.IsMatch(from, _currencyPattern) && Regex.IsMatch(to, _currencyPattern)
                && decimal.TryParse(am.ToString(), out decimal amount);
        }
    }
}
