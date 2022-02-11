using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Model
{
    public class CartAllDetails
    {
        public long UserId { get; set; }  
        
        public long BookId { get; set; }    

        public string BookTitle { get; set; }

        public string BookAuthor { get; set; }
        
        public int OriginalPrice { get; set; }

        public int DiscountedPrice { get; set; }

        public int QtyToOrder { get; set; }

        public float Rating { get; set; }

        public int RatingCount { get; set; }

    }
}
