using BusinessLayer.Interfaces;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

namespace BookStoreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackBL _feedbackBL;
        private IConfiguration _iconfig;
        public FeedbackController(IFeedbackBL feedbackBL, IConfiguration iconfig)
        {

            _feedbackBL = feedbackBL;
            _iconfig = iconfig;
        }

        [Authorize]
        [HttpPost("{bookId}")]
        public IActionResult AddingFeedback(long bookId, FeedbackModel model)
        {
            long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
            var result = this._feedbackBL.AddingFeedback(bookId, model, userId);


            return this.Ok(new { Success = true, message = "Feedback details", Data = result });
        }

        [Authorize]
        [HttpGet("feedback/{bookId}")]
        public IActionResult GetAllReviews(long bookId)
        {
            var result = this._feedbackBL.GetAllReviews(bookId);

            return this.Ok(new { Success = true, message = "All feedback details", result });

        }
    }
}
