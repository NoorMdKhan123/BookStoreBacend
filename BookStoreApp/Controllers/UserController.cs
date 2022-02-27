using BusinessLayer;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Security.Claims;

namespace BookStoreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]


    public class UserController : ControllerBase
    {
        private readonly IUserBL _userBL;
        private IConfiguration _iconfig;


        public UserController(IUserBL userBL, IConfiguration iconfig)
        {

            _userBL = userBL;
            _iconfig = iconfig;
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterResponseModel user)
        {
           
            var result = this._userBL.Registration(user);


            return this.Ok(new { Success = true, message = "Registration Succesful", Data = user });
        }

        [HttpPost("login")]
        public IActionResult GetLogin(UserLogin credentials)
        {
            
            var result = this._userBL.GetLogin(credentials);


            return this.Ok(new { Success = true, message = "Logged in Succesful", result });
        }
        [HttpGet("allusers")]
        public IActionResult GetAllUsers()
        {
            var userDetails = this._userBL.GetAllUsers();
            return this.Ok(new { Success = true, message = "User records found", userdata = userDetails });

        }

        [HttpPost("forgotpassword")]
        public IActionResult ForgotPassword(ForgetResponse forgetResponse)
        {
            string result = this._userBL.ForgotPassword(forgetResponse);

            return this.Ok(new { Success = true, Message = "User records found", Data = result });

        }
        [Authorize]
        [HttpPut("resetpassword")]
        public IActionResult ResetPassword(ResetPasswordModel resetPasswordModel)
        {
            string emailId = User.FindFirst(ClaimTypes.Email).Value.ToString();
         
            string result = this._userBL.ResetPassword(resetPasswordModel, emailId);

            return this.Ok(new { Success = true, Message = "Password got updated", Data = result });
        }
        [Authorize]
        [HttpDelete("{Id}")]
        public IActionResult DeleteRecord(long Id)
        {
            string result = this._userBL.DeleteRecord(Id);

            return this.Ok(new { Success = true, Message = "Record got deleted", Data = result });

        }

        [Authorize]
        [HttpPut("{Id}")]
        public IActionResult UpdateRecord(long Id, UpdateResponse model)
        {
            var result = this._userBL.UpdateRecord(Id, model);

            return this.Ok(new { Success = true, Message = "Record got deleted", Data = result });

        }

    }
}
