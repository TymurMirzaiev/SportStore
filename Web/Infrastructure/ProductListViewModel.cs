using Core.DTOs;
using System.Collections.Generic;
using Web.Models.ViewModels;

namespace Web.Infrastructure
{
    public class ProductsListViewModel
    {
        public IEnumerable<ProductDto> Products { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string CurrentCategory { get; set; }
    }
}
