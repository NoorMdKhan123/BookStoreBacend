using BusinessLayer.Interfaces;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interfaces;
using System;
using System.Linq;

namespace BookStoreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartBL _cartBL;
        private IConfiguration _iconfig;

        public CartController(ICartBL cartBL, IConfiguration iconfig)
        {

            _cartBL = cartBL;
            _iconfig = iconfig;
        }
        [Authorize]
        [HttpPost("addingbooks")]
        public IActionResult AddingBook(BookCart model)
        {
        
            long userId = Convert.ToInt64(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);

            var result = this._cartBL.AddingBook(model, userId);

            return this.Ok(new { Success = true, Message = "Book got added to cart", Data = result });
        }

        [Authorize]
        [HttpPut("cartId")]
        public IActionResult  UpdateCart(BookUpdateModel model, long cartId)
        {
            var result = this._cartBL.UpdateCart(model, cartId);

            return this.Ok(new { Success = true, Message = "details got updated in cart", Data = result });

        }
        [Authorize]
        [HttpGet]
        public IActionResult GetAllCartData()
        {
            long userId = Convert.ToInt64(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);

            var result = this._cartBL.GetAllCartData(userId);

            return this.Ok(new { Success = true, Message = "Book got added to cart", Data = result });

        }

        [Authorize]
        [HttpDelete("{cartId}")]
        public IActionResult DeleteCartDetails(long cartId)
        {
            long userId = Convert.ToInt64(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);

            var result = this._cartBL.DeleteCartDetails(userId);

            return this.Ok(new { Success = true, Message = "details from cart got deleted" });

        }



    }
}
