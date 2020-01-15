using Core.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces.Services
{
    public interface IProductService
    {
        IEnumerable<ProductDto> GetAll();
        ProductDto GetById(int productId);
        ProductDto Remove(ProductDto product);
        void SaveOrder(ProductDto product);
    }
}
