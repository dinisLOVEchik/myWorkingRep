using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Ocsp;
using PersonalFinance.Services;

namespace PersonalFinance.Api.Controllers
{
    [Route("api/fx/convert")]
    [ApiController]
    public class FxController : ControllerBase
    {
        private readonly FxRatesProviderResolver _fxRatesProviderResolver;
        private readonly CurrencyValidator _currencyValidator;

        public FxController(FxRatesProviderResolver fxRatesProviderResolver, CurrencyValidator currencyValidator)
        {
            _fxRatesProviderResolver = fxRatesProviderResolver;
            _currencyValidator = currencyValidator;
        }

        [HttpPost]
        public IActionResult Convert([FromBody] ConversionRequest request)
        {
            if (!_currencyValidator.ValidateRequest(request.CurrencyFrom, request.CurrencyTo, request.Amount))
                return BadRequest("The request data was entered incorrectly! Try again.");
            var fxRatesProvider = _fxRatesProviderResolver.Resolve(request.FxRatesSource);
            var converter = new CurrencyConverter(fxRatesProvider);
            var rate = converter.Convert(request.CurrencyFrom, request.CurrencyTo, request.Amount);
            var source = converter.GetRateProviderSource();
            var response = new
            {
                rate,
                source
            };
            return Ok(response);

        }

        [HttpGet]
        public IActionResult Rates([FromQuery]string rates_source="default param") 
        {
            try
            {
                var fxRatesProvider = _fxRatesProviderResolver.Resolve(rates_source);

                var converter = new CurrencyConverter(fxRatesProvider);
                return Ok(new { rate_source = converter.GetAll() });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound($"Failed to resolve FxRatesProvider for source: {rates_source}. {ex.Message}");
            }
        }
    }
}
