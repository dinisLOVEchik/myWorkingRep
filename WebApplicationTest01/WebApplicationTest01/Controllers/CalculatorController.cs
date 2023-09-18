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
            //decimal res = calcRequest.operation switch
            //{
            //op.add => calcRequest.arg1 + calcRequest.arg2,
            //op.substruct => calcRequest.arg1 - calcRequest.arg2,
            //op.multiply => calcRequest.arg1 * calcRequest.arg2,
            //op.devide => calcRequest.arg1 / calcRequest.arg2
            //};
            if (calcRequest.operation == op.add)
                res = calcRequest.arg1 + calcRequest.arg2;
            else if (calcRequest.operation == op.substruct)
                res = calcRequest.arg1 - calcRequest.arg2;
            else if (calcRequest.operation == op.multiply)
                res = calcRequest.arg1 * calcRequest.arg2;
            else if (calcRequest.operation == op.devide)
                res = calcRequest.arg1 / calcRequest.arg2;

            return $"{res}";
        }
    }
}


public class CalcRequest
{
    public int arg1 { get; set; }
    public int arg2 { get; set; }
    public op operation { get; }
    [JsonConstructor]
    CalcRequest(int arg1, int arg2, op operation)
    {
        this.arg1 = arg1;
        this.arg2 = arg2;
        this.operation = operation;
    }
}

public enum op
{
    add,
    substruct,
    multiply,
    devide
}
