using Microsoft.AspNetCore.Mvc;

namespace IaeBoraAPI.Controllers
{
    [ApiController]
    [Route("status")]
    public class StatusController : Controller
    {
        [HttpGet]
        [Route("")]
        public IActionResult GetStatus()
        {
            return Ok(new { message = "A API está rodando corretamente. " });
        }
    }
}
