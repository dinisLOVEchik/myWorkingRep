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
            var regexPattern = "^[A-Z]{3}$";

            if (Regex.IsMatch(request.CurrencyFrom, regexPattern) || Regex.IsMatch(request.CurrencyTo, regexPattern))
            {
                var rate = _currencyConverter.Convert(request.CurrencyFrom, request.CurrencyTo, request.Amount);
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
    }
}
