using Data.Entities;

namespace Data.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        void SaveOrder(Order order);
        Order GetWithLines(int id);
    }
}
