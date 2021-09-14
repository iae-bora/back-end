using IaeBoraLibrary.Model;
using IaeBoraLibrary.Service;
using Microsoft.AspNetCore.Mvc;

namespace IaeBoraAPI.Controllers
{
    [ApiController]
    [Route("v0/answers")]
    public class AnswerController : Controller
    {
        [HttpGet]
        [Route("{userId}")]
        public IActionResult GetAnswers(string userId)
        {
            return Ok(AnswerService.GetAnswers(userId));
        }

        [HttpPost]
        [Route("")]
        public IActionResult SaveAnswers([FromBody] Answer answer)
        {
            AnswerService.SaveAnswers(answer);
            return Ok();
        }

        [HttpPut]
        [Route("")]
        public IActionResult UpdateAnswers([FromBody] Answer answer)
        {
            AnswerService.UpdateAnswers(answer);
            return Ok();
        }
    }
}
