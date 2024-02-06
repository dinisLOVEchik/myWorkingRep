﻿using Microsoft.AspNetCore.Mvc;
using PersonalFinance.Services;

namespace PersonalFinance.Api.Controllers
{
    [Route("api/fx/convert")]
    [ApiController]
    public class FxController : ControllerBase
    {
        private readonly FxRatesProviderResolver _fxRatesProviderResolver;
        private readonly CurrencyValidator _currencyValidator;
        private readonly SnakeCaseConverter _snakeCase;

        public FxController(FxRatesProviderResolver fxRatesProviderResolver, CurrencyValidator currencyValidator, SnakeCaseConverter snakeCase)
        {
            _fxRatesProviderResolver = fxRatesProviderResolver;
            _currencyValidator = currencyValidator;
            _snakeCase = snakeCase;
        }

        [HttpPost]
        public IActionResult Convert([FromBody] ConversionRequest request)
        {
            if (_currencyValidator.ValidateRequest(request.CurrencyFrom, request.CurrencyTo, request.Amount))
            {
                var fxRatesProvider = _fxRatesProviderResolver.Resolve(request.FxRatesSource);
                var converter = new CurrencyConverter(fxRatesProvider);
                var rate = converter.Convert(request.CurrencyFrom, request.CurrencyTo, Int32.Parse(request.Amount));
                var source = _snakeCase.ToSnakeCase(converter.GetRateProviderSource());
                var response = new
                {
                    rate, source
                };
                return Ok(response);
            }
            return BadRequest("The request data was entered incorrectly! Try again.");
        }
    }
}
