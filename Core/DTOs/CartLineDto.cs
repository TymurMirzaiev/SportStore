using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DTOs
{
    public class CartLineDto
    {
        public int CartLineID { get; set; }
        public ProductDto Product { get; set; }
        public int Quantity { get; set; }
    }
}
