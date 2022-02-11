using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.Model
{
    public class LoginResponseModel
    {
        public long UserId { get; set; }

    
        public string FullName { get; set; }

    
        public string EmailId { get; set; }

     
        public string MobileNumber { get; set; }

        public string Token { get; set; }
    }
}
