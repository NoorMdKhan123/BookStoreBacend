using BusinessLayer.Interfaces;
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
    public class WishListController : ControllerBase
    {
        private readonly IWishListBL _wishlistBL;
        private IConfiguration _iconfig;

        public WishListController(IWishListBL wishlistBL, IConfiguration iconfig)
        {

            _wishlistBL = wishlistBL;
            _iconfig = iconfig;
        }
        [Authorize]
        [HttpPost("{bookId}")]
        public IActionResult WishListCreation(long bookId)
        {
            long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
            var result = this._wishlistBL.WishListCreation(bookId, userId);


            return this.Ok(new { Success = true, message = "book added to wishlist"});
        }

        [Authorize]
        [HttpDelete("del/{wishListId}")]
        public IActionResult DeletWishList(long wishListId)
        {
            var result = this._wishlistBL.DeletWishList(wishListId);


            return this.Ok(new { Success = true, message = "delete from wishlist" });

        }

        [Authorize]
        [HttpGet]
        public IActionResult GetWishlistDetailsByUserId()
        {
            long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);

            var wishlist = this._wishlistBL.GetWishlistDetailsByUserId(userId);

                return this.Ok(new { Success = true, message = "all details of wishlist", wishlist });
        }


    }
}
