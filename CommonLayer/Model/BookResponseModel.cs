using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Model
{
    public class BookResponseModel
    {
        public long UserId { get; set; }
        public string BookTitle { get; set; }
        public string BookAuthor { get; set; }

        public float Rating { get; set; }

        public int RatingCount { get; set; }

        public int BookQty { get; set; }

        public int OriginalPrice { get; set; }

        public string Comment { get; set; }

        public int DiscountedPrice { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }
    }
}
