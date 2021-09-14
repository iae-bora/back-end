using IaeBoraLibrary.Model;
using IaeBoraLibrary.Service;
using Microsoft.AspNetCore.Mvc;

namespace IaeBoraAPI.Controllers
{
    [ApiController]
    [Route("v0/users")]
    public class UserController : Controller
    {
        [HttpGet]
        [Route("{userId}")]
        public IActionResult GetUser(string userId)
        {
            return Ok(UserService.GetUser(userId));
        }

        [HttpPost]
        [Route("")]
        public IActionResult CreateUser([FromBody] User user)
        {
            UserService.CreateUser(user);
            return Ok();
        }
    }
}
 