using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PersonalFinance.Api.Controllers

{
    [Route("api/server/ping")]
    [ApiController]
    public class ServerController : ControllerBase
    {
        [HttpGet]
        public string text1()
        {
            return "pong";
        }
    }
}
