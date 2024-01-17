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

        public FxController(CurrencyConverter currencyConverter)
        {
            _currencyConverter = currencyConverter;
        }

        [HttpPost]
        public IActionResult Convert([FromBody] ConversionRequest request)
        {
            if (ValidateRequest(request))
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

            if (Regex.IsMatch(request.CurrencyFrom, currencyPattern) && Regex.IsMatch(request.CurrencyTo, currencyPattern)
                && int.TryParse(request.Amount, out int amount))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
