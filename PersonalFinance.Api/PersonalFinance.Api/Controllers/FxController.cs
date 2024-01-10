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
            var pattern = "^[A-Z]{3}$";

            if (Regex.IsMatch(request.CurrencyFrom, pattern) || Regex.IsMatch(request.CurrencyTo, pattern))
            {
                var rate = _currencyConverter.Convert(request.CurrencyFrom, request.CurrencyTo, request.Amount);
                return Ok(rate);
            }
            else
                return BadRequest("The request data was entered incorrectly! Try again.");
        }

        private bool IsSupportedCurrency(string currency)
        {

            return true;
        }
    }
}
