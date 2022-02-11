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
    public class AddressController : ControllerBase
    {

           IAddressBL _addressBL;
           IConfiguration _iconfig;

        public AddressController(IAddressBL addressBL, IConfiguration iconfig)
        {
            _addressBL = addressBL;
            _iconfig = iconfig;
        }

        [Authorize]
        [HttpPost]
        public IActionResult AddingAddressDetails(AddressModel model)
        {
            long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);

            var result = this._addressBL.AddingAddressDetails(model, userId);

            return this.Ok(new { Success = true, message = "Added address succesfully", Data = result });
        }

        [Authorize]
        [HttpPut]
        public IActionResult  UpdateAddress(AddressModel model)
        {
            long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);

            var result = this._addressBL.UpdateAddress(model, userId);

            return this.Ok(new { Success = true, message = "Added address succesfully", Data = result });

        }

        [Authorize]
        [HttpGet]

        public IActionResult GetAllAddres()
        {
            long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);

            var result = this._addressBL.GetAllAddres(userId);

            return this.Ok(new { Success = true, message = "Below are all the address details", Data = result});

        }
    }
}
