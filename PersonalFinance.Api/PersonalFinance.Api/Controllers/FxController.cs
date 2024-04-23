using Microsoft.AspNetCore.Mvc;
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
        public RateResponse[] Rates()//([FromBody] string rates_source) 
        {
            CsvRateProvider csvRate = new CsvRateProvider("./data/Output.csv", ';', 10000);
            RateResponse[] array1 = new RateResponse[csvRate.rates().Count];
            for (int i = 0; i < csvRate.rates().Count; i++)
            {
                string value1 = csvRate.rates()[i][0];
                string value2 = csvRate.rates()[i][1];
                string value3 = csvRate.rates()[i][2];
                array1[i] = new RateResponse(value1, value2, value3);
            }
            return array1;
        }
    }
}
