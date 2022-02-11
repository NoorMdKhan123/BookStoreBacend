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
    public class BookController : ControllerBase
    {

        private readonly IBookBL _bookBL;
        private IConfiguration _iconfig;

        public BookController(IBookBL bookBL, IConfiguration iconfig)
        {

            _bookBL = bookBL;
            _iconfig = iconfig;
        }

        [Authorize]
        [HttpPost]
        public IActionResult AddingBookDetails([FromBody]BookModel bookModel)
        {

            long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
            var result = this._bookBL.AddingBookDetails(bookModel, userId);


            return this.Ok(new { Success = true, message = "Registration Succesful", Data = result });
        }

        [Authorize]
        [HttpPut("{bookId}")]
        public IActionResult UpdateBookDetails(BookDetailsModel model, long bookId)
        {
            var result = this._bookBL.UpdateBookDetails(model, bookId);


            return this.Ok(new { Success = true, message = "Registration Succesful", Data = result });

        }


        [Authorize]
        [HttpGet("{bookId}")]

        public IActionResult GetBookDetailsById(long bookId)
        {
            long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);

            var result = this._bookBL.GetBookDetailsById(bookId, userId);


            return this.Ok(new { Success = true, message = "Registration Succesful", Data = result });

        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAllBookDeatils()
        {
            

            var result = this._bookBL.GetAllBookDeatils();


            return this.Ok(new { Success = true, message = "Registration Succesful", Data = result });

        }
        [Authorize]
        [HttpDelete("{bookId}")]
       public IActionResult DeletBookRecord(long bookId)
        {
        

            var result = this._bookBL.DeletBookRecord(bookId);
            return this.Ok(new { Success = true, message = "Record deleted"});
        }


    }








}
