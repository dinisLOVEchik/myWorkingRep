using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplicationTest01.Controllers

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
