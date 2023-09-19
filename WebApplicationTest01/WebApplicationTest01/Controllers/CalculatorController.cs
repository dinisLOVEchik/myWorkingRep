using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using WebApplicationTest01.Controllers;

namespace WebApplicationTest01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculatorController : ControllerBase
    {
        CalculatorService calculatorService = new CalculatorService();
        [HttpPost]
        public string Calc(CalcRequest calcRequest)
        {
            return calculatorService.Calc(calcRequest.arg1, calcRequest.arg2, calcRequest.op);
        }
    }
}
