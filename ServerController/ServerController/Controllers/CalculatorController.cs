using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ServerController.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculatorController : ControllerBase
    {
        [HttpPost]
        public string Calc(int arg1, int arg2)
        {
            decimal result = arg1 + arg2;
            return $"{result}";
        }
    }
}
