using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Ocsp;
using PersonalFinance.Services;
using System.Text.Json.Nodes;

namespace PersonalFinance.Api.Controllers
{
    [Route("api/fx")]
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
        [Route("convert")]
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
        [Route("rates")]
        public IActionResult Rates([FromQuery] string rates_source = null)
        {
            try
            {
                Dictionary<string, List<CurrencyExchangeRate>> ratesBySource = GetRatesBySource(rates_source);
                if (ratesBySource.Any())
                {
                    return Ok(ratesBySource);
                }
                else
                {
                    return NotFound($"No rates found for source: {rates_source}");
                }
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound($"Failed to resolve FxRatesProvider for source: {rates_source}. {ex.Message}");
            }
        }
        #region PRIVATE METHODS FOR RATES
        private Dictionary<string, List<CurrencyExchangeRate>> GetRatesBySource(string rates_source)
        {
            Dictionary<string, List<CurrencyExchangeRate>> ratesBySource = new Dictionary<string, List<CurrencyExchangeRate>>();
            if (string.IsNullOrEmpty(rates_source))
            {
                foreach (var provider in _fxRatesProviderResolver.GetProviders().Values)
                {
                    if (provider.IsAvailable())
                    {
                        AddRatesFromProvider(provider, ratesBySource);
                    }
                }
            }
            else
            {
                var fxRatesProvider = _fxRatesProviderResolver.Resolve(rates_source);
                AddRatesFromProvider(fxRatesProvider, ratesBySource);
            }
            return ratesBySource;
        }
        private void AddRatesFromProvider(IRateProvider provider, Dictionary<string, List<CurrencyExchangeRate>> ratesBySource)
        {
            var currencyExchangeRate = new CurrencyExchangeRateRepository(provider);
            switch (provider)
            {
                case CsvRateProvider:
                    ratesBySource.Add("CSV", currencyExchangeRate.GetAll().ToList());
                    break;
                case MySqlRateProvider:
                    ratesBySource.Add("MySql", currencyExchangeRate.GetAll().ToList());
                    break;
                case SqlServerRateProvider:
                    ratesBySource.Add("MSSQL", currencyExchangeRate.GetAll().ToList());
                    break;
            }
        }
        #endregion
    }
}
