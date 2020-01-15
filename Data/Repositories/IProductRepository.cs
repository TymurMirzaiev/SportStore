using Data.Entities;

namespace Data.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        void SaveOrder(Product product);
    }
}
