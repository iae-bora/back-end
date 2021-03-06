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
        [Route("{userId}")]
        public IActionResult GetLastRoute(string userId)
        {
            return Ok(RouteService.GetLastRouteJson(userId));
        }

        [HttpGet]
        [Route("all/{userId}")]
        public IActionResult GetAllRoutes(string userId)
        {
            return Ok(RouteService.GetAllRoutesJson(userId));
        }

        [HttpPost]
        [Route("")]
        public IActionResult CreateRoute([FromBody] Answer answer)
        {
            return Ok(RouteService.CreateDetailedRoute(answer));
        }
    }
}
