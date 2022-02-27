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
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersBL _ordersBL;
        private IConfiguration _iconfig;

        public OrdersController(IOrdersBL ordersBL, IConfiguration iconfig)
        {

            _ordersBL = ordersBL;
            _iconfig = iconfig;
        }

        [Authorize]
        [HttpPost]
        public IActionResult AddingOrderDetails(OrderModel model)
        {
            long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
            var result = this._ordersBL.AddingOrderDetails(model, userId);


            return this.Ok(new { Success = true, message = "Order Details added", Data = result });
        }


        [Authorize]
        [HttpGet]
        public IActionResult GetAllCartData()
        {
            long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
            var result = this._ordersBL.GetAllCartData(userId);


            return this.Ok(new { Success = true, message = "Order data got  fetched", Data = result });

        }

    }
}
