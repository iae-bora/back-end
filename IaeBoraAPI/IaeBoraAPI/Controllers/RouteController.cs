using IaeBoraLibrary.Model;
using IaeBoraLibrary.Service;
using Microsoft.AspNetCore.Mvc;

namespace IaeBoraAPI.Controllers
{
    [ApiController]
    [Route("v0/Routes")]
    public class RouteController : Controller
    {
        [HttpGet]
        [Route("")]
        public IActionResult GetLastRoute()
        {
            throw new System.NotImplementedException("Implementar");
        }

        [HttpGet]
        [Route("all")]
        public IActionResult GetAllRoutes()
        {
            throw new System.NotImplementedException("Implementar");
        }

        [HttpPost]
        [Route("")]
        public IActionResult CreateRoute([FromBody] Answer answer)
        {
            return Ok(RouteService.CreateDetailedRoute(answer));
        }

        [HttpPut]
        [Route("")]
        public IActionResult UpdateRoute()
        {
            throw new System.NotImplementedException("Implementar");
        }
    }
}
