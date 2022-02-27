using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Model
{
    public class OrderResModel
    {
        public long UserId { get; set; }
        public long OrderId { get; set; }
        public long AddressId { get; set; }
        public long BookId { get; set; }
        public int Price { get; set; }
        public int OrderQuantity { get; set; }
        
    }
}
