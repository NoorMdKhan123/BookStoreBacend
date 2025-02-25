﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Model
{
    public class CartModel
    {
        public long CartId { get; set; }
        public long BookId { get; set; }

        public long UserId { get; set; }    

        public int QtyToOrder { get; set; }

        public BookDetailsModel bookModel { get; set; }
    }
}
