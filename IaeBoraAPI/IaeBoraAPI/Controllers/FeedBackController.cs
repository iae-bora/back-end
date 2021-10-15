using IaeBoraLibrary.Model;
using IaeBoraLibrary.Service;
using Microsoft.AspNetCore.Mvc;

namespace IaeBoraAPI.Controllers
{
    [ApiController]
    [Route("v0/feedback")]
    public class FeedBackController : Controller
    {
        [HttpGet]
        [Route("{userId}")]
        public IActionResult GetFeedBack(string userId)
        {
            return Ok(FeedBackService.GetAllFeedBacks(userId));
        }

        [HttpPost]
        [Route("")]
        public IActionResult CreateFeedBack([FromBody] FeedBack feedBack)
        {
            FeedBackService.CreateFeedBack(feedBack);
            return Ok();
        }

        [HttpDelete]
        [Route("{feedBackId}/{userId}")]
        public IActionResult DeleteFeedBack(int feedBackId, string userId)
        {
            FeedBackService.DeleteFeedBack(feedBackId, userId);
            return Ok();
        }
    }
}
