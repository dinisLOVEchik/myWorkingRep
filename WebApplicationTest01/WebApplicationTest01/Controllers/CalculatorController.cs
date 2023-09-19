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
        [HttpPost]
        public string Calc(CalcRequest calcRequest)
        {
            decimal res = 0;
            if (calcRequest.op.Equals(Operation.add.ToString()))
                res = calcRequest.arg1 + calcRequest.arg2;
            else if (calcRequest.op.Equals(Operation.substruct.ToString()))
                res = calcRequest.arg1 - calcRequest.arg2;
            else if (calcRequest.op.Equals(Operation.multiply.ToString()))
                res = calcRequest.arg1 * calcRequest.arg2;
            else if (calcRequest.op.Equals(Operation.devide.ToString()))
                res = calcRequest.arg1 / calcRequest.arg2;

            return $"{res}";
        }
    }
}
