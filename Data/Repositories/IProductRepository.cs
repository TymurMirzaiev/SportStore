using Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories
{
    public interface IProductRepository: IRepository<Product>
    {
        void SaveOrder(Product product);
    }
}
