using Microsoft.AspNetCore.Mvc;
using PersonalFinance.Services;
using System.Text.RegularExpressions;

namespace PersonalFinance.Api.Controllers
{
    [Route("api/fx/convert")]
    [ApiController]
    public class FxController : ControllerBase
    {
        private readonly CurrencyConverter _currencyConverter;
        private readonly CurrencyValidator _currencyValidator;

        public FxController(CurrencyConverter currencyConverter, CurrencyValidator currencyValidator)
        {
            _currencyConverter = currencyConverter;
            _currencyValidator = currencyValidator;
        }

        [HttpPost]
        public IActionResult Convert([FromBody] ConversionRequest request)
        {
            if (_currencyValidator.ValidateRequest(request.CurrencyFrom, request.CurrencyTo, request.Amount))
            {
                var rate = _currencyConverter.Convert(request.CurrencyFrom, request.CurrencyTo, Int32.Parse(request.Amount));
                var source = _currencyConverter.GetRateProviderSource();
                var response = new
                {
                    rate, source
                };
                return Ok(response);
            }
            else
                return BadRequest("The request data was entered incorrectly! Try again.");
        }

        private bool ValidateRequest(ConversionRequest request)
        {
            var currencyPattern = "^[A-Z]{3}$";

            return Regex.IsMatch(request.CurrencyFrom, currencyPattern) && Regex.IsMatch(request.CurrencyTo, currencyPattern)
                && int.TryParse(request.Amount, out int amount);
        }
    }
}
