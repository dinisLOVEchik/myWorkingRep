using Microsoft.AspNetCore.Mvc;
using PersonalFinance.Services;

namespace PersonalFinance.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculatorController : ControllerBase
    {
        private readonly CalculatorService _calculatorService;
        public CalculatorController()
        {
            _calculatorService = new CalculatorService();
        }

        [HttpPost]
        public decimal Calc(CalcRequest calcRequest)
        {
            return _calculatorService.Calc(calcRequest.arg1, calcRequest.arg2, calcRequest.op);
        }
    }
}
