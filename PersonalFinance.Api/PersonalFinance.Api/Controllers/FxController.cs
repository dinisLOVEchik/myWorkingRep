using Microsoft.AspNetCore.Mvc;
using PersonalFinance.Services;

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
        public string Convert(CurrenciesRequest currenciesRequest)
        {
            return _currencyConverter.Convert(currenciesRequest.CurrencyFrom, currenciesRequest.CurrencyTo, currenciesRequest.Amount) + "";
        }
    }
}
