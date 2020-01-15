using Data.Context;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Repositories
{
    public class OrderRepository: EFGenericRepository<Order>, IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
            :base(context)
        {
            _context = context;
        }

        public void SaveOrder(Order order)
        {
            _context.AttachRange(order.Lines.Select(l => l.Product));
            if (order.OrderId == 0)
            {
                _context.Orders.Add(order);
            }
            _context.SaveChanges();
        }

        public Order GetWithLines(int id)
        {
            var orders = _context.Orders
                .Include(o => o.Lines)
                    .ThenInclude(l => l.Product)
                .FirstOrDefault(o => o.OrderId == id);

            return orders;
        }
    }
}
