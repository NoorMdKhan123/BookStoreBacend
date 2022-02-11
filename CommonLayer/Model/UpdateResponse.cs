using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Model
{
    public class UpdateResponse
    {
        public long UserId { get; set; }

        public string FullName { get; set; }


        public string EmailId { get; set; }


        public string Passowrd { get; set; }


        public string MobileNumber { get; set; }
    }
}
