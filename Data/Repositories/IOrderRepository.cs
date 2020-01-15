using Data.Context;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories
{
    public interface IOrderRepository: IRepository<Order>
    {
        void SaveOrder(Order order);
        Order GetWithLines(int id);
    }
}
