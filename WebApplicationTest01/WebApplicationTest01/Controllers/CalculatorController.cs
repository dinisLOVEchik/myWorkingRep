using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplicationTest01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculatorController : ControllerBase
    {
        [HttpPost]
        public string Calc(int arg1, int arg2, string command)
        {
            decimal res = 0;
            if (command == "add")
                res = arg1 + arg2;
            else if (command == "substruct")
                res = arg1 - arg2;
            else if (command == "multiply")
                res = arg1 * arg2;
            else if (command == "devide")
                res = arg1 / arg2;

            return $"{res}";
        }
    }
}
